using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Templates;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using CAPA_NEGOCIO.Utility;
using DataBaseModel;
using MailKit;

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
				if (pariente.Actualizo == true)
				{

					return GetUpdatedData(pariente, periodoLectivo);
				}
				else
				{
					var estudiantes = new Estudiantes().Where<Estudiantes>(
							FilterData.In("Id", pariente?.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
						).Where(e => e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null).ToList();

					/*var familia = new Estudiantes_responsables_familia{Pariente_id = pariente?.Id}
						.Get<Estudiantes_responsables_familia>();*/

					//List<int?>? familiasId = pariente?.Estudiantes_responsables_familia?.Select(f => f.Familia_id).Distinct().ToList();

					List<Parientes>? parientes = estudiantes
						.SelectMany(e => e.Responsables ?? [])
						.Select(r => r.Parientes ?? new Parientes())
						.DistinctBy(p => p.Id)
						.ToList();

					UpdateData updateData = new UpdateData
					{
						Estudiantes = estudiantes.Select(e => new Estudiantes_Data_Update(e)).ToList(),
						Parientes = parientes.Select(e => new Parientes_Data_Update(e)).ToList()
					};
					GetBoletaContracts(updateData);

					return updateData;
				}

			}
			return new UpdateData
			{
				Estudiantes = [],
				Parientes = []
			};

		}

		private static void GetBoletaContracts(UpdateData updateData)
		{
			try
			{
				updateData.Contrato = new DocumentsData().GetContratoFragment(updateData)?.Body;
				updateData.Boleta = new DocumentsData().GetBoletaFragment(updateData)?.Body;
			}
			catch (System.Exception ex)
			{
				updateData.Contrato = HtmlContentGetter.ReadHtmlFile("contratotemplate.html", "Resources");
				updateData.Boleta = HtmlContentGetter.ReadHtmlFile("boleta.html", "Resources");
			}
		}

		private static UpdateData GetUpdatedData(Parientes_Data_Update? pariente, Periodo_lectivos? periodoLectivo)
		{
			var estudiantes = new Estudiantes_Data_Update().Where<Estudiantes_Data_Update>(
										FilterData.In("Id", pariente?.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
									).Where(e => e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null).ToList();

			/*var familia = new Estudiantes_responsables_familia{Pariente_id = pariente?.Id}
				.Get<Estudiantes_responsables_familia>();*/

			var parientesId = estudiantes?.SelectMany(e => e.Responsables ?? []).Select(f => f.Pariente_id).Distinct().ToArray();

			List<Parientes_Data_Update>? parientes = new Parientes_Data_Update().Where<Parientes_Data_Update>(
					FilterData.In("Id", parientesId)
				).ToList();

			UpdateData updateData = new UpdateData
			{
				Estudiantes = estudiantes,
				Parientes = parientes
			};
			GetBoletaContracts(updateData);
			//updateData.Contrato = new DocumentsData().GetBoletaFragment(updateData)?.Body;
			return updateData;
		}

		public ResponseService StartUpdateProcess(UpdateData updateData)
		{
			List<Parientes_Data_Update>? parientes = [];
			if (updateData.SendAll != null)
			{
				parientes = new Parientes { Responsable_Pago = true }.Get<Parientes>().Select(p => new Parientes_Data_Update(p)).ToList();
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
						pariente.Correo_enviado = null;
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
							Password = StringUtil.GenerateRandomPassword(),
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
			//inst.filterData?.Add(FilterData.Limit(100));
			//inst.Responsable_Pago = true;
			var clases = new Estudiante_clases
			{
				filterData = [FilterData.In("Periodo_lectivo_id", Periodo_lectivos.PeriodoActivo()?.Id)]
			}.SimpleGet<Estudiante_clases>();
			
			var estudiantes = new Estudiantes
			{
				filterData = [FilterData.In("Id", clases.Select(x => x.Estudiante_id).ToArray())]
			}.SimpleGet<Estudiantes>();			
			
			inst.filterData?.Add(FilterData.NotNull("User_id"));
			inst.filterData?.Add(FilterData.NotIn("Id", new Parientes_Data_Update().SimpleGet<Parientes_Data_Update>().Select(x => x.Id).ToArray()));
			inst.filterData?.Add(FilterData.In("Id_familia", estudiantes.Select(x => x.Id_familia).ToArray()));
			return inst.SimpleGet<Parientes>();
		}
		public static List<Parientes_Data_Update>? GetParientesQueLoguearon(Parientes_Data_Update inst)
		{
			//inst.filterData?.Add(FilterData.Limit(100));
			inst.Entro_al_sistema = true;
			//inst.filterData?.Add(FilterData.Equal("Entro_al_sistema", 1));

			return inst.SimpleGet<Parientes_Data_Update>();
		}
		public static List<Parientes_Data_Update>? GetParientesQueActulizaron(Parientes_Data_Update inst)
		{
			//inst.filterData?.Add(FilterData.Limit(100));
			inst.Actualizo = true;
			//inst.filterData?.Add(FilterData.Equal("Actualizo", 1));
			return inst.SimpleGet<Parientes_Data_Update>();
		}
		public static List<Parientes_Data_Update>? GetParientesInvitados(Parientes_Data_Update inst)
		{
			//inst.filterData?.Add(FilterData.Limit(100));
			inst.filterData?.Add(FilterData.NotNull("User_id"));
			return inst.SimpleGet<Parientes_Data_Update>();
		}

		public ResponseService Save(string? seassonKey, UpdateDataRequest inst)
		{
			UserModel user = AuthNetCore.User(seassonKey);
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
							pariente.Actualizo = user.UserId == parienteF.User_id ? true : false;
							pariente.Acepto_terminos = true;
							pariente.User_id = parienteF.User_id;
							pariente.Update();
						}
						else
						{
							pariente.Estudiantes_responsables_familia = null;
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
							estudiante.Responsables = null;
							estudiante.Estudiante_clases = null;
							if (estudiante?.Puntos_Transportes?.Count > 0)
							{
								estudiante.Usa_transporte = true;
							}
							estudiante?.Save();
						}
					});
					CommitGlobalTransaction();
					try
					{
						Parientes_Data_Update? pariente = new Parientes_Data_Update { User_id = user.UserId }.Find<Parientes_Data_Update>();
						MailServices.SendMailAceptedContract(pariente, GetUpdateData(seassonKey));
					}
					catch (System.Exception ex)
					{
						LoggerServices.AddMessageError("Error al enviar correo de actualizacion", ex);
					}
					return new ResponseService { status = 200, message = "Datos actualizados!" };

				}
				else
				{
					return new ResponseService { status = 403, message = "Debe aceptar los terminos y condiciones" };
				}
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("Error al guardar la informacion", ex);
				throw;
			}

		}

		public void sendInvitations()
		{

			var tutor = new Parientes_Data_Update();			
			var filter = FilterData.Or(
				FilterData.ISNull("correo_enviado"),
				FilterData.Equal("correo_enviado", false)
			);

			//var tutores = tutor.Where<Parientes_Data_Update>(filter);
			tutor.filterData?.Add(FilterData.NotNull("User_id"));
			var tutores = tutor.Where<Parientes_Data_Update>(filter);

			tutores.ForEach(t =>
			{
				try
				{
					Security_Users? usuario = new Security_Users().Find<Security_Users>(FilterData.Equal("id_user", t.User_id));

					var plantillaString = HtmlContentGetter.ReadHtmlFile("invitacionTemplate.html", "Resources");
					var template = TemplateServices.RenderTemplateInvitacion(plantillaString, usuario, t.Nombre_completo);

					MailServices.SendMailInvitation(new List<String>() { t.Email }, null, "Actualización de datos", template, new { numero_contrato = 123 } as dynamic);
					BeginGlobalTransaction();
					t.Correo_enviado = true;
					t.Update();
					CommitGlobalTransaction();
				}
				catch (System.Exception ex)
				{
					LoggerServices.AddMessageError("Error al enviar correo de invitacion correo:", ex);
				}
			});



		}

		public static UpdateData GetUpdateDataById(Parientes_Data_Update inst)
		{
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();
			Parientes_Data_Update? pariente = new Parientes_Data_Update { Id = inst.Id }.Find<Parientes_Data_Update>();
			if (pariente?.Actualizo == true)
			{
				return GetUpdatedData(pariente, periodoLectivo);
			}
			return new UpdateData();
		}

		public static List<Parientes_Data_Update>? GetParientesQueNoLoguearon(Parientes_Data_Update inst)
		{
			inst.filterData?.Add(FilterData.Limit(100));
			inst.filterData?.Add(FilterData.ISNull("Entro_al_sistema"));
			inst.filterData?.Add(FilterData.NotNull("User_id"));
			//inst.filterData?.Add(FilterData.Equal("Entro_al_sistema", 1));
			return inst.SimpleGet<Parientes_Data_Update>();
		}
		public static string GenerateRandomPassword(int length = 8)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			var password = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				password.Append(chars[random.Next(chars.Length)]);
			}

			return password.ToString();
		}
	}
}