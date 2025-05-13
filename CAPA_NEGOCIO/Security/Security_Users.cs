using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Gestion_Pagos.Operations;
using DataBaseModel;

namespace CAPA_NEGOCIO
{
	public class Security_Users : CAPA_DATOS.Security.Security_Users
	{
		public new Tbl_Profile? Tbl_Profile { get; set; }
		public static object Login(UserModel Inst, string? idetify)
		{
			var usere = AuthNetCore.loginIN(Inst.mail, Inst.password, idetify);
			var user = AuthNetCore.User(idetify);
			user.message = usere.message;

			Tbl_Profile? profile = new Tbl_Profile();
			if (user.status == 200)
			{
				profile = Tbl_Profile.Get_Profile(user);
				try
				{
					if(profile.IsPariente) 
					{
					    new PagosOperation().UpdatePagosFromBellacon(idetify);
					}					
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError($"error actualizando pagos {user.mail}", ex);
				}
			}
			return new
			{
				UserId = user.UserId,
				mail = user.mail,
				password = user.password,
				status = user.status,
				success = user.success,
				isAdmin = user.isAdmin,
				message = user.message,
				permissions = user.permissions,
				Direccion = profile.Direccion,
				Profesion = profile.Profesion,
				Telefono = profile.Telefono,
				Celular = profile.Celular,
				Codigo_Familia = profile.Codigo_Familia,
				Correo = profile.Correo_institucional,
				Foto = profile.Foto
			};
		}

		public new Tbl_Profile Get_Profile()
		{
			return Tbl_Profile.Get_Profile(Id_User.GetValueOrDefault(), this);
		}

		public Security_Users withConection(CAPA_DATOS.BDCore.Abstracts.WDataMapper mapper)
		{
			this.SetConnection(mapper);
			return this;
		}
	}
	public class Tbl_Profile : CAPA_DATOS.Security.Tbl_Profile
	{
		public string? Direccion { get; set; }
		public string? Profesion { get; set; }
		public string? Telefono { get; set; }
		public string? Celular { get; set; }
		public string? Codigo_Familia { get; set; }
		public ProfileType? ProfileType { get; set; }
		public List<Parientes?>? FamiliaTutores { get; set; }
		public List<Estudiantes?>? FamiliaEstudiantes { get; set; }
		public int? Pariente_id { get; private set; }
		public int? Docente_id { get; private set; }
        public bool IsPariente { get; internal set; }

        public static Tbl_Profile Get_Profile(UserModel User)
		{
			return Get_Profile(User.UserId.GetValueOrDefault(), User.UserData);
		}

		public static Tbl_Profile Get_Profile(int UserId, CAPA_DATOS.Security.Security_Users user)
		{
			Docentes? docente = new Docentes { Id_User = UserId }.SimpleFind<Docentes>();
			Parientes? pariente = new Parientes { User_id = UserId }.Find<Parientes>();
			Tbl_Profile? tbl_Profile = new Tbl_Profile { IdUser = UserId }.SimpleFind<Tbl_Profile>();
			bool isPariente = false;
			if (docente == null && pariente == null)
			{
				tbl_Profile.ProfileType = CAPA_NEGOCIO.ProfileType.USER;
				//return tbl_Profile;
				return new Tbl_Profile(){
					Nombres = user.Nombres,
					Correo_institucional = user.Mail,
					Foto = "/media/img/avatar.png"
				};
			}

			List<Estudiantes_responsables_familia> familia = [];
			if (pariente != null && pariente.Estudiantes_responsables_familia != null)
			{
				List<int> ids = pariente.Estudiantes_responsables_familia
					.Select(x => x.Familia_id.GetValueOrDefault())
					.Distinct().ToList();
				ids.ForEach(id => familia.AddRange(new Estudiantes_responsables_familia { Familia_id = id }
					.Where<Estudiantes_responsables_familia>(
						FilterData.Distinc("pariente_id", pariente.Id)))
				);
				isPariente = true;				
			}
			List<Parientes?> parientes = familia.Select(f => f.Parientes)
							.DistinctBy(p => p!.Id)
							.ToList();
			List<Estudiantes?> estudiantes = familia.Select(f => f.Estudiantes)
							.DistinctBy(p => p!.Id)
							.ToList();

			return new Tbl_Profile
			{
				ProfileType = docente != null ? CAPA_NEGOCIO.ProfileType.DOCENTE : (pariente != null ? CAPA_NEGOCIO.ProfileType.PARIENTE : CAPA_NEGOCIO.ProfileType.USER),
				Nombres = docente != null ? docente.Nombre_completo : pariente?.Nombre_completo ?? user.Nombres,
				Foto = GetAvatar(docente, pariente, tbl_Profile),
				Direccion = docente != null ? docente.Direccion : pariente?.Direccion,
				Correo_institucional = docente != null ? docente.Email : pariente?.Email,
				Profesion = docente != null ? docente.Escolaridades?.Nombre : "",
				Telefono = docente != null ? docente.Telefono : pariente?.Telefono,
				Celular = docente != null ? docente.Celular : pariente?.Celular,
				Codigo_Familia = familia.FirstOrDefault()?.Familia_id?.ToString("D9") ?? "",
				FamiliaTutores = parientes,
				Sexo = docente != null ? docente.Sexo : pariente?.Sexo ?? "M",
				FamiliaEstudiantes = estudiantes,
				IdUser = UserId,
				Pariente_id = pariente?.Id,
				Docente_id = docente?.Id,
				IsPariente = true,
				Id_Perfil = tbl_Profile?.Id_Perfil
			};
		}
		
		

		private static string GetAvatar(Docentes? docente, Parientes? pariente, Tbl_Profile? tbl_Profile)
		{
			string sexo = docente?.Sexo?.ToUpper() ?? pariente?.Sexo?.ToUpper() ?? "M";
			var pageConfig = Config.pageConfig();

			return (docente != null && docente.Foto == null) || (pariente != null && pariente.Foto == null)
			? sexo == "M" ? pageConfig.MEDIA_IMG_PATH + "avatar.png" : pageConfig.MEDIA_IMG_PATH + "avatar_fem.png"
			: (docente != null
				? $"/Media/Images/maestros/{docente.Id}/{docente.Foto}"
				: $"{pariente?.Foto}");
		}
	}


	public enum ProfileType
	{
		DOCENTE, ESTUDIANTE, PARIENTE,
		USER
	}
}