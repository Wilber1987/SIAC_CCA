using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Operations
{
	public class UpdateOperation : TransactionalClass
	{
		public static UpdateData GetUpdateData(string seassonKey)
		{
			UserModel user = AuthNetCore.User(seassonKey);
			Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();

			var estudiantes = new Estudiantes().Where<Estudiantes>(
				FilterData.In("Id", pariente?.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
			).Where(e => e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null).ToList();

			/*var familia = new Estudiantes_responsables_familia{Pariente_id = pariente?.Id}
				.Get<Estudiantes_responsables_familia>();*/

			//List<int?>? familiasId = pariente?.Estudiantes_responsables_familia?.Select(f => f.Familia_id).Distinct().ToList();

			List<Parientes>? parientes = estudiantes
				.SelectMany(e => e.Responsables ?? [])
				.Select(r => r.Parientes ?? new Parientes()).ToList();

			return new UpdateData
			{
				Estudiantes = estudiantes,
				Parientes = parientes
			};
		}

		public ResponseService StartUpdateProcess(UpdateData updateData)
		{
			List<Parientes>? parientes = [];
			if (updateData.SendAll != null)
			{
				parientes = new Parientes { Responsable_Pago = true }.Get<Parientes>();
			} else
			{
				parientes = updateData.Parientes;
			}
			if (parientes == null || parientes.Count == 0)
			{
				return new ResponseService
				{
					status = 403,
					message = "No hay datos para actualizar"	
				};
			}
			
			try
			{
				BeginGlobalTransaction();
				parientes?.ForEach(tn =>
				{
					var rolPadreResponsable = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "PADRE_RESPONSABLE"));
					var user = (Security_Users?)new Security_Users
					{
						Nombres = tn.Nombre_completo,
						Estado = "ACTIVO",
						Descripcion = tn.Nombre_completo,
						Password = StringUtil.GeneratePassword(tn.Email, tn.Primer_nombre, tn.Primer_apellido),
						Mail = StringUtil.GenerateNickName(tn.Primer_nombre, tn.Primer_apellido),
						Token = null,
						Password_Expiration_Date = DateTime.Now.AddDays(30),
						Security_Users_Roles = [new Security_Users_Roles { Security_Role = rolPadreResponsable, Estado = "ACTIVO" }]
					}.Save_User(null);
					Parientes_Data_Update parientes_Data_Update = (Parientes_Data_Update)tn;
					parientes_Data_Update.User_id = user?.Id_User;
					parientes_Data_Update.Save();
				});
				CommitGlobalTransaction();
				return new ResponseService { status = 200, message = "Actualizacion enviado" };

			}
			catch (System.Exception)
			{
				RollBackGlobalTransaction();
				throw;
			}
		}

		public static ResponseService UpdateEstudiante(string? seassonKey, Estudiantes_Data_Update inst)
		{
			UserModel user = AuthNetCore.User(seassonKey);
			Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var estudiante = new Estudiantes_Data_Update { Id = inst.Id }.SimpleFind<Estudiantes_Data_Update>();
			if (pariente?.Responsable_Pago == true)
			{
				if (estudiante != null)
				{
					return inst.Update();
				}
				else
				{
					var saveResponse = inst.Save();
					return new ResponseService
					{
						status = 200,
						message = "Datos actualizados",
						body = saveResponse
					};
				}

			}
			return new ResponseService
			{
				status = 403,
				message = "No tiene permisos para realizar esta accion"
			};

		}

		public static ResponseService UpdateParientes(string? seassonKey, Parientes_Data_Update inst)
		{
			UserModel user = AuthNetCore.User(seassonKey);
			Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var parienteData = new Parientes_Data_Update { Id = inst.Id }.SimpleFind<Parientes_Data_Update>();
			if (pariente?.Responsable_Pago == true)
			{
				if (parienteData != null)
				{
					return inst.Update();
				}
				else
				{
					var saveResponse = inst.Save();
					return new ResponseService
					{
						status = 200,
						message = "Datos actualizados",
						body = saveResponse
					};
				}
			}
			return new ResponseService
			{
				status = 403,
				message = "No tiene permisos para realizar esta accion"
			};
		}
	}
}