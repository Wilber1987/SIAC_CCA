using API.Controllers;
using CAPA_DATOS;
using CAPA_NEGOCIO.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
	public class Parientes : EntityClass
	{
		#region Propiertys
		[PrimaryKey(Identity = true)]
		public int? Id { get; set; }
		public string? Primer_nombre { get; set; }
		public string? Segundo_nombre { get; set; }
		public string? Primer_apellido { get; set; }
		public string? Segundo_apellido { get; set; }
		public string? Sexo { get; set; }		
		public string? Direccion { get; set; }
		public string? Lugar_trabajo { get; set; }
		public string? Telefono { get; set; }
		public string? Celular { get; set; }
		public string? Foto { get; set; }
		public string? Telefono_trabajo { get; set; }
		public string? Email { get; set; }
		public int? Estado_civil_id { get; set; }		
		public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }
		public int? Id_Titulo { get; set; }
		
		
		public bool? Responsable_Pago { get; set; }
		public String? Ex_Alumno { get; set; }
		public DateTime? Fecha_Nacimiento { get; set; }
		public DateTime? Created_at { get; set; }// es fecha grabacion
		public DateTime? Fecha_Modificacion { get; set; }// es fecha grabacion
		public String? Usuario_Grabacion { get; set; }
		public String? Usuario_Edicion { get; set; }
		public Double? Ejercicio { get; set; }
		public bool? Actualizado { get; set; }
		public int? No_Responsable { get; set; }
		public int? Id_familia { get; set; }	
	   	public int? Id_relacion_familiar { get; set; }
		public int? User_id { get; set; }
		public String? Identificacion { get; set; }
		public int? Id_pais { get; set; }
		public int? Id_Region { get; set; }
		public int? Id_Estado_Civil { get; set; }
		public int? Religion_id { get; set; }
		public int? Id_profesion { get; set; }

		[JsonProp]
		public List<ProfileRequest>? ProfileRequest { get; internal set; }

		// [ManyToOne(TableName = "Security_Users", KeyColumn = "Id_User", ForeignKeyColumn = "User_id")]

		[OneToMany(TableName = "Estudiantes_responsables_familia", KeyColumn = "Id", ForeignKeyColumn = "Pariente_id")]
		public List<Estudiantes_responsables_familia>? Estudiantes_responsables_familia { get; set; }
		
		[ManyToOne(TableName = "religiones", KeyColumn = "id", ForeignKeyColumn = "Religion_id")]
		public Religiones? Religion { get; set; }

		[ManyToOne(TableName = "estados_civiles", KeyColumn = "id", ForeignKeyColumn = "Id_Estado_Civil")]
		public Estados_Civiles? Estado_civil { get; set; }

		[ManyToOne(TableName = "regiones", KeyColumn = "id_region", ForeignKeyColumn = "Id_Region")]
		public Regiones? Region { get; set; } 

		[ManyToOne(TableName = "paises", KeyColumn = "id_pais", ForeignKeyColumn = "id_pais")]
		public Paises? Pais { get; set; } 

		[ManyToOne(TableName = "profesiones", KeyColumn = "id_profesion", ForeignKeyColumn = "id_profesion")]
		public Profesiones? Profesion { get; set; } 

		[ManyToOne(TableName = "titulos", KeyColumn = "id_titulo", ForeignKeyColumn = "id_titulo")]
		public Titulos? Titulos { get; set; } 


		#endregion

		public static List<Estudiantes> GetOwEstudiantes(string? identity, Estudiantes estudiante)
		{
			UserModel user = AuthNetCore.User(identity);
			Parientes? pariente = new Parientes{User_id = user.UserId}.Find<Parientes>();
			if (pariente?.Estudiantes_responsables_familia != null)
			{
				var periodoLectivo = Periodo_lectivos.PeriodoActivo();
				return estudiante.Where<Estudiantes>(
					FilterData.In("Id",	pariente.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
				).Where(e => e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null).ToList();
			}
			//throw new Exception("No posee estudiantes asociados");
			return [];
		}

		public object GetResponsables()
		{
			var parientes = Where<Parientes>(FilterData.Limit(30), FilterData.NotNull("User_id"));
			return parientes.Select(Pariente => new {
				Pariente.Id,
				Pariente.Primer_nombre,
				Pariente.Segundo_nombre,
				Pariente.Primer_apellido,
				Pariente.Segundo_apellido,
				Pariente.Sexo,
				Pariente.Telefono,
				Pariente.Celular,
				Pariente.Telefono_trabajo,
				Pariente.Email,
				Pariente.User_id
			}).ToList();
		}
	}
}
