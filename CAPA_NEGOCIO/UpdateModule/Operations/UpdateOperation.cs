using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Security;
using CAPA_NEGOCIO.Services;
using CAPA_NEGOCIO.Templates;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using AppCore.Services;
using DataBaseModel;
using MailKit;
using APPCORE.Util;
using APPCORE.Services;

namespace CAPA_NEGOCIO.UpdateModule.Operations
{
	public class UpdateOperation : TransactionalClass
	{
		public static UpdateData? GetOwUpdateData(string sessionKey)
		{
			UserModel user = AuthNetCore.User(sessionKey);
			Parientes? parienteE = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();

			Parientes_Data_Update? pariente = new Parientes_Data_Update
			{
				Id = parienteE?.Id,
				Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto
			}.Find<Parientes_Data_Update>();
			if (pariente == null)
			{
				pariente = new Parientes_Data_Update();
				AdapterUtil.SetMatchingProperties(parienteE, pariente);
				pariente.Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto;
				pariente.Save(fullInsert: false);
			}
			if (pariente?.Estudiantes_responsables_familia != null)
			{
				if (pariente.Actualizo == true)
				{
					return GetUpdatedData(pariente, periodoLectivo, true);
				}
				else
				{
					var estudiantes = new Estudiantes().Where<Estudiantes>(
							FilterData.In("Id", pariente?.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
						).Where(e => e.Estudiante_clases?.Find(ec => ec.Periodo_lectivo_id == periodoLectivo?.Id) != null).ToList();

					List<Parientes>? parientes = estudiantes
						.SelectMany(e => e.Responsables ?? [])
						.Select(r => r.Parientes ?? new Parientes())
						.DistinctBy(p => p.Id)
						.ToList();

					UpdateData updateData = new UpdateData
					{
						Estudiantes = estudiantes.Select(e => new Estudiantes_Data_Update(e)).ToList(),
						Parientes = parientes.Select(e => new Parientes_Data_Update(e)).ToList(),
						ParientesId = parientes.Select(e => e.Id.GetValueOrDefault()).ToList(),
					};
					GetBoletaContracts(updateData, pariente);
					return updateData;
				}

			}

			var updatedDataQuery = new UpdatedData().Find<UpdatedData>(
				FilterData.JsonPropEqual("DataContract", "Id_Tutor_responsable", parienteE?.Id),
				FilterData.JsonPropEqual("DataContract", "Year", periodoLectivo?.Nombre_corto)
			);
			return new UpdateData
			{
				Estudiantes = [],
				Parientes = [],
				UpdatedData = updatedDataQuery
			};

		}

		private static void GetBoletaContracts(UpdateData updateData, Parientes_Data_Update pariente)
		{
			try
			{
				updateData.Contrato = new DocumentsData().GetContratoFragment(updateData, pariente)?.Body;
				updateData.Boleta = new DocumentsData().GetBoletaFragment(updateData, pariente)?.Body;
			}
			catch (System.Exception)
			{
				updateData.Contrato = HtmlContentGetter.ReadHtmlFile("contratotemplate.html", "Resources");
				updateData.Boleta = HtmlContentGetter.ReadHtmlFile("boleta.html", "Resources");
			}
		}

		private static UpdateData? GetUpdatedData(Parientes_Data_Update? pariente, Periodo_lectivos? periodoLectivo, bool onlyRetenidos = false)
		{

			var updatedDataQuery = new UpdatedData().Find<UpdatedData>(
				FilterData.JsonPropEqual("DataContract", "Id_Tutor_responsable", pariente?.Id),
				FilterData.JsonPropEqual("DataContract", "Year", periodoLectivo?.Nombre_corto)
			);

			var estudiantes = new Estudiantes_Data_Update().Where<Estudiantes_Data_Update>(
				FilterData.In("Id", pariente?.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray()),
				FilterData.Equal("Periodo_Lectivo_Update", periodoLectivo?.Nombre_corto)
			);

			var parientesId = estudiantes?.SelectMany(e => e.Responsables ?? [])
				.Select(f => f.Pariente_id).Distinct().ToArray();

			List<Parientes_Data_Update>? parientes = new Parientes_Data_Update().Where<Parientes_Data_Update>(
					FilterData.In("Id", parientesId),
					FilterData.Equal("Periodo_Lectivo_Update", periodoLectivo?.Nombre_corto)
			).ToList();

			/* solo estudiantes que fueron retenidos al momento de actualizar, 
			 * es posible que estos dejen de estar retenidos a posterior, 
			 * son los unicos que estaran filtrados*/
			var estudiantesRetenidos = updatedDataQuery?.HaveRetenidos == true && onlyRetenidos ?
			 new Estudiantes().Where<Estudiantes>(
				FilterData.In("Id", updatedDataQuery?.DataContract?.EstudiantesRetenidos?.ToArray())
			) : [];

			UpdateData updateData = new UpdateData
			{
				Estudiantes = updatedDataQuery?.HaveRetenidos == true ? estudiantesRetenidos.Select(e => new Estudiantes_Data_Update(e)).ToList() : estudiantes,
				EstudiantesRetenidos = estudiantesRetenidos.Select(e => new Estudiantes_Data_Update(e)).ToList(),
				Parientes = updatedDataQuery?.HaveRetenidos == false ? parientes : [],
				ParientesId = parientes.Select(e => e.Id.GetValueOrDefault()).ToList(),
				UpdatedData = updatedDataQuery
			};
			GetBoletaContracts(updateData, pariente);
			//updateData.Contrato = new DocumentsData().GetBoletaFragment(updateData)?.Body;
			return updateData;
		}

		public ResponseService StartUpdateProcess(UpdateData updateData)
		{
			if (true)
			{
				return new ResponseService
				{
					status = 200,
					message = "Esta opción esta deshabilitada, debido a nuevos requerimientos"
				};
			}
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
					//MailServices.SendMailAceptedContract(pariente, GetUpdateData(sessionKey));
					Parientes_Data_Update? pariente = new Parientes_Data_Update { Id = tn.Id }.Find<Parientes_Data_Update>();
					if (pariente != null)
					{
						pariente.Correo_enviado = false;
						pariente.Acepto_terminos = false;
						var user = new Security_Users { Id_User = pariente.User_id }.SimpleFind<Security_Users>();
						user!.Password = EncrypterServices.Encrypt(StringUtil.GenerateRandomPassword());
						user?.Update();
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
				return new ResponseService { status = 200, message = "Solicitudes de actualización enviadas" };

			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError("Error en StartUpdateProcess", ex);
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

		public static ResponseService UpdateEstudiante(string? sessionKey, Estudiantes_Data_Update inst)
		{
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();
			UserModel user = AuthNetCore.User(sessionKey);
			Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var estudiante = new Estudiantes_Data_Update
			{
				Id = inst.Id,
				Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto
			}.SimpleFind<Estudiantes_Data_Update>();
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

		public static ResponseService UpdateParientes(string? sessionKey, Parientes_Data_Update inst)
		{
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();
			UserModel user = AuthNetCore.User(sessionKey);
			Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
			var parienteData = new Parientes_Data_Update
			{
				Id = inst.Id,
				Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto
			}.SimpleFind<Parientes_Data_Update>();
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

		public static List<ViewParientesUpdate>? GetParientesToInvite(Parientes inst)
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
			//return inst.SimpleGet<Parientes>();

			var parientes = new ViewParientesUpdate
			{
				filterData = new List<FilterData>
				{
					FilterData.Equal("Entro_al_sistema", true),
					FilterData.NotNull("User_id"),
					FilterData.NotIn("Id", new Parientes_Data_Update().SimpleGet<Parientes_Data_Update>().Select(x => x.Id).ToArray()),
					FilterData.In("Id_familia", estudiantes.Select(x => x.Id_familia).ToArray())
				}
			};

			return parientes.SimpleGet<ViewParientesUpdate>();
		}
		public static List<ViewParientesUpdate>? GetParientesQueLoguearon(Parientes_Data_Update inst)
		{
			UpdateFechaActualizacion();
			var parientes = new ViewParientesUpdate();
			return parientes.Where<ViewParientesUpdate>(FilterData.Equal("Entro_al_sistema", true));
		}
		public static List<ViewParientesUpdate>? GetParientesQueActulizaron(Parientes_Data_Update inst)
		{
			UpdateFechaActualizacion();
			var parientes = new ViewParientesUpdate();
			return parientes.Where<ViewParientesUpdate>(FilterData.Equal("Actualizo", true));
		}
		public static List<ViewParientesUpdate>? GetParientesInvitados(Parientes_Data_Update inst)
		{
			UpdateFechaActualizacion();
			//inst.filterData?.Add(FilterData.Limit(100));
			var parientes = new ViewParientesUpdate();
			return parientes.Where<ViewParientesUpdate>(FilterData.NotNull("User_id"));
			/*inst.filterData?.Add(FilterData.NotNull("User_id"));
			return inst.SimpleGet<Parientes_Data_Update>();*/
		}

		public ResponseService Save(string? sessionKey, UpdateDataRequest inst)
		{
			UserModel user = AuthNetCore.User(sessionKey);
			try
			{
				var periodoLectivo = Periodo_lectivos.PeriodoActivo();

				if (inst.AceptaTerminosYCondiciones == true)
				{
					BeginGlobalTransaction();
					List<Estudiantes_Data_Update> retenidos = [];
					inst.Parientes?.ForEach(pariente =>
					{
						Parientes_Data_Update? parienteF = new Parientes_Data_Update
						{
							Id = pariente.Id,
							Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto
						}.Find<Parientes_Data_Update>();

						if (parienteF != null)
						{
							pariente.Actualizo = true;
							pariente.Acepto_terminos = true;
							pariente.User_id = parienteF.User_id;
							pariente.Fecha_actualizacion = DateTime.Now;
							pariente.Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto;
							pariente.Update();
						}
						else
						{
							pariente.Estudiantes_responsables_familia = null;
							pariente.Fecha_actualizacion = DateTime.Now;
							pariente.Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto;
							pariente.Save();
						}
					});
					inst.Estudiantes?.ForEach(estudiante =>
					{
						if (estudiante?.Retenido == true)
						{
							retenidos.Add(estudiante);
							return;
						}
						Estudiantes_Data_Update? estudianteF = new Estudiantes_Data_Update
						{
							Id = estudiante?.Id,
							Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto
						}.Find<Estudiantes_Data_Update>();

						if (estudianteF != null)
						{
							estudiante!.Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto;
							estudiante.Update();
						}
						else
						{
							estudiante!.Responsables = null;
							estudiante.Estudiante_clases = null;
							estudiante.Periodo_Lectivo_Update = periodoLectivo?.Nombre_corto;
							if (estudiante?.Puntos_Transportes?.Count > 0)
							{
								estudiante.Usa_transporte = true;
							}
							estudiante?.Save();
						}
					});
					try
					{
						Parientes_Data_Update? pariente = new Parientes_Data_Update { User_id = user.UserId }.Find<Parientes_Data_Update>();
						SaveUpdateData(pariente, GetOwUpdateData(sessionKey), retenidos);
						CommitGlobalTransaction();
					}
					catch (Exception ex)
					{
						RollBackGlobalTransaction();
						LoggerServices.AddMessageError("Error al enviar correo de actualizacion (userid: " + user.UserId + ") ", ex);
					}
					return new ResponseService { status = 200, message = "¡Datos actualizados!" };

				}
				else
				{
					RollBackGlobalTransaction();
					return new ResponseService { status = 403, message = "Debe aceptar los terminos y condiciones" };
				}
			}
			catch (Exception ex)
			{
				RollBackGlobalTransaction();
				LoggerServices.AddMessageError("Error al guardar la informacion", ex);
				throw;
			}

		}

		public static async void SaveUpdateData(Parientes_Data_Update tutor, UpdateData updateData, List<Estudiantes_Data_Update> retenidos)
		{

			string templatePage = "<div><h1> Contrato aceptado y datos actualizados</h1><p>Hemos adjuntado los contratos y boletas, favor descarguelos</p></div>";
			List<ModelFiles> Attach_Files = [];
			ModelFiles boleta = new ModelFiles();
			ModelFiles contrato = new ModelFiles();
			if (updateData.Contrato != null && updateData.Contrato != "")
			{
				contrato = FileService.HtmlToPdfBase64(updateData.Contrato, "contrato_.pdf");
				Attach_Files.Add(contrato);
			}
			if (updateData.Boleta != null && updateData.Boleta != "")
			{
				boleta = FileService.HtmlToPdfBase64(updateData.Boleta, "boleta_.pdf");
				Attach_Files.Add(boleta);
			}
			foreach (var file in Attach_Files ?? new List<ModelFiles>())
			{
				ModelFiles? Response = (ModelFiles?)FileService.upload("Attach\\", file).body;
				file.Value = Response?.Value;
				file.Type = Response?.Type;
			}

			try
			{
				/*var updatedDataQuery = new UpdatedData().Find<UpdatedData>(
					FilterData.JsonPropEqual("DataContract", "Id_Tutor_responsable", tutor?.Id),
					FilterData.JsonPropEqual("DataContract", "Year", periodoLectivo?.Nombre_corto)
				);*/
				// Primero agregas los estudiantes NO retenidos a la lista Estudiantes
				var idsNoRetenidos = updateData.Estudiantes
					.Where(e => e.Retenido == false)
					.Select(e => e.Id.GetValueOrDefault())
					.ToList();
				if (updateData.UpdatedData != null)
				{
					updateData.UpdatedData.DataContract?.Estudiantes?.AddRange(idsNoRetenidos);
					// Ahora removemos de EstudiantesRetenidos todos los Ids que están en Estudiantes
					var idsEstudiantes = updateData.UpdatedData.DataContract?.Estudiantes?.ToHashSet() ?? new HashSet<int>();
					updateData.UpdatedData.DataContract!.EstudiantesRetenidos =
						updateData.UpdatedData.DataContract.EstudiantesRetenidos
							.Where(id => !idsEstudiantes.Contains(id))
							.ToList();
					updateData.UpdatedData?.Documents_Contracts?.Add(contrato);
					updateData.UpdatedData?.Documents_Boletas?.Add(boleta);
					updateData.UpdatedData?.Update();
				}
				else
				{
					// guardo los archivos con su ruta
					var updatedData = new UpdatedData//todo meter en el try catch solo si se envia el correo
					{
						DataContract = new DataContract
						{
							Id_Tutor_responsable = tutor.Id,
							Tutor_responsable = tutor.Nombre_completo,
							Estudiantes = idsNoRetenidos,
							EstudiantesRetenidos = retenidos.Select(estud => estud.Id.GetValueOrDefault())
								.ToList(),
							Tutores = updateData.Parientes.Select(p => p.Id.GetValueOrDefault()).ToList(),
							Fecha = DateTime.Now,
							Year = DateTime.Now.Year.ToString(),
						},
						Documents_Contracts = [contrato],
						Documents_Boletas = [boleta]
					}.Save();
				}
				//ENVIO DE CORREO
				await MailServices.SendContractMail(tutor, templatePage, Attach_Files);
			}
			catch (Exception ex)
			{
				LoggerServices.AddMessageError($"error guardando los archivos", ex);
			}

		}
		/// <summary>
		/// envio de invitaciones
		/// </summary>

		public void sendInvitations()
		{
			var tutor = new Parientes_Data_Update();
			var filter = FilterData.Or(
				FilterData.Distinc("correo_enviado", true),
				FilterData.Equal("correo_enviado", false),
				FilterData.ISNull("correo_enviado")
			);

			tutor.filterData?.Add(FilterData.NotNull("User_id"));
			//tutor.filterData?.Add(FilterData.Equal("Id", 2508));
			tutor.filterData?.Add(FilterData.Limit(25));
			var tutores = tutor.Where<Parientes_Data_Update>(filter);

			tutores.ForEach(t =>
			{
				try
				{
					BeginGlobalTransaction();

					Security_Users? usuario = new Security_Users().Find<Security_Users>(FilterData.Equal("id_user", t.User_id));
					usuario!.Password = StringUtil.GenerateRandomPassword();
					usuario!.Password_Expiration_Date = DateTime.Now.AddDays(60);
					var save = usuario?.Save_User(null);



					var plantillaString = HtmlContentGetter.ReadHtmlFile("invitacionTemplate.html", "Resources");
					var template = TemplateServices.RenderTemplateInvitacion(plantillaString, usuario, t.Nombre_completo);
					string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
					string subject = $"Actualización de datos {currentDate.Replace("/", "-")}";

					MailServices.SendMail(new List<String>() { t.Email }, null, subject, template);

					t.Correo_enviado = true;
					t.Update();
					CommitGlobalTransaction();
				}
				catch (Exception ex)
				{
					RollBackGlobalTransaction();
					LoggerServices.AddMessageError("Error al enviar correo de invitacion correo:", ex);
				}
			});
		}

		public static UpdateData? GetUpdateDataById(Parientes_Data_Update inst)
		{
			var periodoLectivo = Periodo_lectivos.PeriodoActivo();
			Parientes_Data_Update? pariente = new Parientes_Data_Update { Id = inst.Id }.Find<Parientes_Data_Update>();
			if (pariente?.Actualizo == true)
			{
				return GetUpdatedData(pariente, periodoLectivo);
			}
			return new UpdateData();
		}

		public static List<ViewParientesUpdate>? GetParientesQueNoLoguearon(Parientes_Data_Update inst)
		{
			var parientes = new ViewParientesUpdate();
			parientes.filterData?.Add(FilterData.ISNull("Entro_al_sistema"));
			parientes.filterData?.Add(FilterData.NotNull("User_id"));
			return parientes.Where<ViewParientesUpdate>(FilterData.NotNull("User_id"));
		}

		public static void UpdateFechaActualizacion()
		{
			return;
			/*var parientes = new Parientes_Data_Update().Where<Parientes_Data_Update>(
				FilterData.ISNull("Fecha_actualizacion"),
				FilterData.NotNull("User_id")
			);
			parientes.ForEach(pariente =>
			{
				var updatedDataQuery = new UpdatedData
				{
					filterData = [new FilterData
					{
						ObjectName = "DataContract",
						PropName = "Id_Tutor_responsable",
						FilterType = "JSONPROP_EQUAL",
						PropSQLType = "int",
						Values = new List<string?> { pariente.Id.ToString() },
					}]
				}.Find<UpdatedData>();
				pariente.Fecha_actualizacion = updatedDataQuery?.DataContract?.Fecha;
				pariente.Update();
			});*/
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