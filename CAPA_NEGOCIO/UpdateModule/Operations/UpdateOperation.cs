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
			Parientes_Data_Update? pariente = new Parientes_Data_Update { User_id = user.UserId }.Find<Parientes_Data_Update>();
			Parientes? parienteE = new Parientes { Id = pariente?.Id }.Find<Parientes>();
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();
			if (pariente?.Estudiantes_responsables_familia != null)
			{
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
			return new UpdateData
			{
				Estudiantes = [],
				Parientes = []
			};

		}

		public ResponseService StartUpdateProcess(UpdateData updateData)
		{
			List<Parientes>? parientes = [];
			if (updateData.SendAll != null)
			{
				parientes = new Parientes { Responsable_Pago = true }.Get<Parientes>();
			}
			else
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
					Parientes_Data_Update? pariente = new Parientes_Data_Update { Id = tn.Id }.Find<Parientes_Data_Update>();
					if (pariente != null)
					{
						pariente.Update();
					}
					else
					{
						Security_Roles? rolPadreResponsable = GetActualizadorRol();
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
						Parientes_Data_Update parientes_Data_Update = new Parientes_Data_Update(tn);
						parientes_Data_Update.User_id = user?.Id_User;
						parientes_Data_Update.Save();
					}

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

		private static Security_Roles? GetActualizadorRol()
		{
			Security_Roles? rolPadreResponsable = new Security_Roles().Find<Security_Roles>(FilterData.Equal("descripcion", "ACTUALIZADOR_FAMILIA"));
			if (rolPadreResponsable == null)
			{
				Security_Permissions? permission = new Security_Permissions().Find<Security_Permissions>(FilterData.Equal("descripcion", "UPDATE_FAMILY_DATA"));
				if (permission == null)
				{
					permission = (Security_Permissions?)new Security_Permissions
					{
						Descripcion = "UPDATE_FAMILY_DATA",
						Estado = "ACTIVO",
						Detalles = "PERMITE ACTUALIZAR DATOS DE FAMILIA"
					}.Save();
				}
				rolPadreResponsable = (Security_Roles?)new Security_Roles
				{
					Descripcion = "ACTUALIZADOR_FAMILIA",
					Estado = "ACTIVO",
					Security_Permissions_Roles = [new Security_Permissions_Roles
						{ Security_Permissions = permission, Estado = "ACTIVO" }]
				}.Save();
			}

			return rolPadreResponsable;
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
					inst.Correo_enviado = false;
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

		public static List<Parientes>? GetParientesToInvite(Parientes inst)
		{
			inst.filterData?.Add(FilterData.Limit(100));
			//inst.Responsable_Pago = true;
			inst.filterData?.Add(FilterData.NotNull("User_id"));
			inst.filterData?.Add(FilterData.NotIn("Id", new Parientes_Data_Update().SimpleGet<Parientes_Data_Update>().Select(x => x.Id).ToArray()));
			return inst.SimpleGet<Parientes>();
		}
		public static List<Parientes_Data_Update>? GetParientesQueLoguearon(Parientes_Data_Update inst)
		{
			inst.filterData?.Add(FilterData.Limit(100));
			inst.Entro_al_sistema = true;
			//inst.filterData?.Add(FilterData.Equal("Entro_al_sistema", 1));

			return inst.SimpleGet<Parientes_Data_Update>();
		}
		public static List<Parientes_Data_Update>? GetParientesQueActulizaron(Parientes_Data_Update inst)
		{
			inst.filterData?.Add(FilterData.Limit(100));
			inst.Actualizo = true;
			//inst.filterData?.Add(FilterData.Equal("Actualizo", 1));
			return inst.SimpleGet<Parientes_Data_Update>();
		}
		public static List<Parientes_Data_Update>? GetParientesInvitados(Parientes_Data_Update inst)
		{
			inst.filterData?.Add(FilterData.Limit(100));
			return inst.SimpleGet<Parientes_Data_Update>();
		}

		public ResponseService Save(string? identfy, UpdateDataRequest inst)
		{
			try
			{
				if (inst.AceptaTerminosYCondiciones == true)
				{
					BeginGlobalTransaction();
					inst.Parientes?.ForEach(pariente =>
					{
						Parientes_Data_Update? parienteF = new Parientes_Data_Update { Id = pariente.Id }.Find<Parientes_Data_Update>();
						if (parienteF != null)
						{
							pariente.Update();
						}
						else
						{							
							pariente.Save();
						}
					});
					inst.Estudiantes?.ForEach(estudiante =>
					{
						Estudiantes_Data_Update? estudianteF = new Estudiantes_Data_Update { Id = estudiante.Id }.Find<Estudiantes_Data_Update>();
						if (estudianteF != null)
						{
							estudiante.Update();
						}
						else
						{							
							estudiante.Save();
						}
					});
					CommitGlobalTransaction();
					return new ResponseService { status = 200, message = "solicitud guardada" };

				}
				else
				{
					return new ResponseService { status = 403, message = "Debe aceptar los terminos y condiciones" };
				}
			}
			catch (System.Exception ex)
			{

				throw;
			}


		}
	}
}