using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using APPCORE;
using APPCORE.Services;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using iText.StyledXmlParser.Jsoup.Helper;
using Microsoft.Extensions.Configuration;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class PagosOperation
	{
		private readonly SshTunnelService _sshTunnelService;

		public PagosOperation()
		{
			_sshTunnelService = new SshTunnelService(LoadConfiguration());
		}

		private IConfigurationRoot LoadConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();
		}

		public List<Tbl_Pago> GetPagos(Tbl_Pago pago, string identify)
		{
			//return new List<Tbl_Pago>();
			//List<Estudiantes> estudiantes = UpdatePagosFromBellacon(identify);
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes(), true);

			return new Tbl_Pago
			{
				orderData = [OrdeData.Asc("Fecha")]
			}.Where<Tbl_Pago>(
				FilterData.In("Estudiante_Id", estudiantes.Select(x => Int32.Parse(x.Codigo)).ToArray()),
				//FilterData.ISNull("Fecha_anulacion"),
				FilterData.Equal("Estado", PagosState.PENDIENTE),
				FilterData.Greater("Monto_Pendiente", 0)
			);

		}
		private List<Tbl_Pago> GetPagosEstudiante(int? estudianteId)
		{
			return new Tbl_Pago
			{
				orderData = [OrdeData.Asc("Fecha")]
			}.Where<Tbl_Pago>(
				FilterData.Equal("Estudiante_Id", estudianteId),
				//FilterData.ISNull("Fecha_anulacion"),
				FilterData.Equal("Estado", PagosState.PENDIENTE),
				FilterData.Greater("Monto_Pendiente", 0)
			);
		}

		public List<Estudiantes> UpdatePagosFromBellaconrespaldo(string identify)
		{
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes(), true);
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));
			var pagosP = new Tbl_Pago()
			{
				orderData = [OrdeData.Asc("Fecha")]
			}.Where<Tbl_Pago>(
				FilterData.In("Id_Estudiante", estudiantes.Select(x => x.Id).ToArray())
			);
			List<Pagos_alumnos_view> recientraidos;


			using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
				siacTunnel.Start();

				var pagosAlumnosView = new Pagos_alumnos_view();
				pagosAlumnosView.SetConnection(MySqlConnections.BellacomTest);
				List<FilterData> filters = [FilterData.ISNull("fecha_anulacion"),
					FilterData.In("codigo_estudiante", estudiantes.Select(x => x.Codigo).ToArray()),
					FilterData.Greater("importe_saldo_md", 0)
				];


				if (pagosP.Count > 0)
				{
					filters.Add(FilterData.NotIn("Id_documento_cc", pagosP.Select(x => x.Id_documento_cc).ToArray()));
				}

				recientraidos = pagosAlumnosView.Where<Pagos_alumnos_view>(filters.ToArray()).ToList();

				siacTunnel.Stop();
				siacSshClient.Disconnect();
			}
			var recienTraidosPagos = recientraidos.SelectMany(x =>
			{
				return BuildCuentasPorCobrar(x, responsable);
			}).ToList();
			return estudiantes;
		}

		public List<Estudiantes> UpdatePagosFromBellacon(string identify)
		{
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes(), true);
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));

			var pagosInSqlServer = new Tbl_Pago()
			.Where<Tbl_Pago>(
				FilterData.In("Estudiante_Id", estudiantes.Select(x => x.Codigo).ToArray()),
				FilterData.Equal("Estado", PagosState.PENDIENTE.ToString()),
				FilterData.Greater("Monto_pendiente", 0)
			);

			// 1. Pagos normales
			var pagosAlumnosView = new Pagos_alumnos_view();
			pagosAlumnosView.SetConnection(MySqlConnections.BellacomTest);
			var pagosAlumnosView2 = new Pagos_alumnos_view();
			pagosAlumnosView2.SetConnection(MySqlConnections.BellacomTest);

			// 2. Pagos extraordinarios
			var viewExtra = new ViewPagosAlumnosExtraordinarios();
			viewExtra.SetConnection(MySqlConnections.BellacomTest);
			var viewExtra2 = new ViewPagosAlumnosExtraordinarios();
			viewExtra2.SetConnection(MySqlConnections.BellacomTest);


			List<FilterData> filters = [
				FilterData.In("codigo_estudiante", estudiantes.Select(x => x.Codigo).ToArray()),
					FilterData.Greater("importe_saldo_md", 0),
					FilterData.ISNull("fecha_anulacion")
			];
			List<FilterData> filtersExtraordinarios = [
				FilterData.In("codigo_estudiante", estudiantes.Select(x => x.Codigo).ToArray()),
					FilterData.ISNull("fecha_anulacion"),
					FilterData.Greater("importe_saldo_md", 0)
			//,FilterData.Equal("no_documento", 32011419)
			];
			List<FilterData> filtrosSqlserver = [
				FilterData.In("id_documento_cc", pagosInSqlServer.Select(x => x.Id_documento_cc).ToArray())
			//,FilterData.NotNull("fecha_anulacion")
			];

			List<Pagos_alumnos_view> recientraidos;
			List<ViewPagosAlumnosExtraordinarios> pagosExtraordinarios;
			List<Pagos_alumnos_view> anuladosEnMysql;
			List<ViewPagosAlumnosExtraordinarios> anuladosEnMysqlExtraordinarios;

			using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
				siacTunnel.Start();
				recientraidos = pagosAlumnosView.Where<Pagos_alumnos_view>(filters.ToArray()).ToList();
				pagosExtraordinarios = viewExtra.Where<ViewPagosAlumnosExtraordinarios>(filtersExtraordinarios.ToArray()).ToList();

				anuladosEnMysql = pagosAlumnosView2.Where<Pagos_alumnos_view>(filtrosSqlserver.ToArray()).ToList();
				anuladosEnMysqlExtraordinarios = viewExtra2.Where<ViewPagosAlumnosExtraordinarios>(filtrosSqlserver.ToArray()).ToList();

				siacTunnel.Stop();
				siacSshClient.Disconnect();
			}

			// 3. Unimos las 3 listas
			var allPagos = new List<Pagos_alumnos_view>();
			allPagos.AddRange(recientraidos);
			allPagos.AddRange(pagosExtraordinarios);

			allPagos.AddRange(anuladosEnMysql);
			allPagos.AddRange(anuladosEnMysqlExtraordinarios);


			// se usa todo el conjunto unificado
			var recienTraidosPagos = allPagos.SelectMany(x =>
			{
				return BuildCuentasPorCobrar(x, responsable);
			}).ToList();

			return estudiantes;
		}

		private static List<Tbl_Pago> BuildCuentasPorCobrar(Pagos_alumnos_view x, Tbl_Profile responsable)
		{
			List<Tbl_Pago> pagos = new List<Tbl_Pago>();

			/*
			1024029
			1023112
			*/
			var pagoExistente = new Tbl_Pago().Where<Tbl_Pago>(
				FilterData.Equal("Id_documento_cc", x.Id_documento_cc)
			).FirstOrDefault();

			//string nuevoEstado = x.Fecha_anulacion != null
			PagosState nuevoEstado = (x.Usuario_anulacion != null && x.Fecha_anulacion != null)
				? PagosState.ANULADO
				: PagosState.PENDIENTE;

			if (pagoExistente != null)
			{
				pagoExistente.Monto_Pendiente = x.Importe_saldo_md;

				// Solo actualizar el estado si ha cambiado
				if (pagoExistente.Estado != nuevoEstado)
				{
					pagoExistente.Estado = nuevoEstado;
					pagoExistente.Update();
				}
				pagoExistente.Update();
				pagos.Add(pagoExistente);
			}
			else
			{
				MoneyEnum? moneyEnumValue = MoneyEnum.DOLARES;
				if (x?.Moneda != null)
				{
					Enum.TryParse(x?.Moneda, out MoneyEnum result);
					moneyEnumValue = result;
				}

				var nuevoPago = new Tbl_Pago
				{
					Estudiante_Id = x.Codigo_estudiante,
					Responsable_Id = responsable.Pariente_id,
					Monto = x.Importe_saldo_md,
					Monto_Pagado = 0,
					Monto_Pendiente = x.Importe_saldo_md,
					Documento = x.No_documento.HasValue ? x.No_documento.Value.ToString() : "00001",
					Concepto = x.Texto_posicion,
					Periodo_lectivo = x.Anio?.ToString() ?? "",
					Mes = x.Mes?.ToString() ?? "Enero",
					Money = moneyEnumValue,
					Fecha = x.Fecha_documento ?? DateTime.Now,
					Fecha_Limite = x.Fecha_documento?.AddDays(30) ?? DateTime.Now.AddDays(30),
					Estado = nuevoEstado,

					Id_plazo = x.Id_plazo,
					Anio = x.Anio,
					Id_documento_cc = x.Id_documento_cc,
					Id_documento = x.Id_documento,
					Id_clase_documento = x.Id_clase_documento,
					Fecha_documento = x.Fecha_documento,
					Fecha_contabilizacion = x.Fecha_contabilizacion,
					Ejercicio = x.Ejercicio,
					Periodo = x.Periodo,
					No_documento = x.No_documento,
					Id_deudor = x.Id_deudor,
					Asignacion = x.Asignacion,
					Texto_posicion = x.Texto_posicion,
					Id_cuenta = x.Id_cuenta,
					Id_indicador_impuesto = x.Id_indicador_impuesto,
					Id_documento_detalle = x.Id_documento_detalle,
					Fecha_anulacion = x.Fecha_anulacion,
					Usuario_anulacion = x.Usuario_anulacion,
					Texto_corto = x.Texto_corto,
					Simbolo = x.Simbolo
				};

				nuevoPago.Save();
				pagos.Add(nuevoPago);
			}

			return pagos;
		}

		private static List<Tbl_Pago> BuildCuentasPorCobrarRespaldo(Pagos_alumnos_view x, Tbl_Profile responsable)
		{
			MoneyEnum? moneyEnumValue = MoneyEnum.DOLARES;
			if (x?.Moneda != null)
			{
				Enum.TryParse(x?.Moneda, out MoneyEnum result);
				moneyEnumValue = result;
			}
			List<Tbl_Pago> pagos = new List<Tbl_Pago>
			{

				new Tbl_Pago {
					Estudiante_Id = x.Codigo_estudiante,
					Responsable_Id = responsable.Pariente_id,
					Monto = x.Importe_saldo_md,
					Monto_Pagado = 0,
					Monto_Pendiente = x.Importe_saldo_md,
					Documento = x.No_documento.HasValue ? x.No_documento.Value.ToString() : "00001",
					Concepto = x.Texto_posicion,
					Periodo_lectivo = x.Anio.Value.ToString(),
					Mes = x.Mes.HasValue ? x.Mes.Value.ToString() : "Enero",
					Money = moneyEnumValue,
					Fecha = x.Fecha_documento ?? DateTime.Now,
					Fecha_Limite = x.Fecha_documento?.AddDays(30) ?? DateTime.Now.AddDays(30),
					Estado = PagosState.PENDIENTE,
					// Nuevas propiedades añadidas
					Id_plazo = x.Id_plazo,
					Anio = x.Anio,
					Id_documento_cc = x.Id_documento_cc,
					Id_documento = x.Id_documento,
					Id_clase_documento = x.Id_clase_documento,
					Fecha_documento = x.Fecha_documento,
					Fecha_contabilizacion = x.Fecha_contabilizacion,
					Ejercicio = x.Ejercicio,
					Periodo = x.Periodo,
					No_documento = x.No_documento,
					Id_deudor = x.Id_deudor,
					Asignacion = x.Asignacion,
					Texto_posicion = x.Texto_posicion,
					Id_cuenta = x.Id_cuenta,
					Id_indicador_impuesto = x.Id_indicador_impuesto,
					Id_documento_detalle = x.Id_documento_detalle,
					Fecha_anulacion = x.Fecha_anulacion,
					Usuario_anulacion = x.Usuario_anulacion,
					Texto_corto = x.Texto_corto,
					Simbolo = x.Simbolo
				}
			};
			foreach (var pago in pagos)
			{
				pago.Save();
			}

			return pagos;
		}



		public Object GetPagosAllPagos(Tbl_Pago pago, string identify)
		{
			//return new List<Tbl_Pago>();
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes(), false);
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));
			pago.orderData = [OrdeData.Asc("Fecha")];
			return new
			{
				Estudiantes = estudiantes,
				Pagos = pago.Where<Tbl_Pago>(
					FilterData.In("Estudiante_Id", estudiantes.Select(x => x.Codigo).ToArray()),
					FilterData.Equal("Estado", PagosState.PENDIENTE),
					FilterData.ISNull("Fecha_anulacion")
				)
			};
		}


		public static ResponseService SetPagosRequest(PagosRequest pagosRequest, string? identify)
		{
			try
			{
				#region validar pagos

				var pagosEstudianteSeleccionados = pagosRequest.Detalle_Pago.GroupBy(p => p.Pago?.Estudiante_Id);
				foreach (var grupo in pagosEstudianteSeleccionados)
				{
					int? estudianteId = grupo.Key; // El ID del estudiante
					List<Tbl_Pago> pagos = new PagosOperation().GetPagosEstudiante(estudianteId);
					var pagosEstudiantesPendientes = pagos.Where(x => x.Estudiante_Id == estudianteId).ToList();
					// Iterar por cada pago de ese estudiante
					foreach (var pago in grupo)
					{
						if (ValidarPagos(pago.Pago, grupo.ToList(), pagosEstudiantesPendientes))
						{
							return new ResponseService(403, "Debe cancelar los pagos anteriores.");
						}
					}
				}
				#endregion

				pagosRequest.Monto = pagosRequest.Detalle_Pago!.Sum(x => x.Total);
				pagosRequest.Moneda = pagosRequest.Detalle_Pago.First().Pago!.Money.ToString();

				SessionServices.Set("PagosRequest", pagosRequest, identify);
				return new ResponseService
				{
					status = 200,
					message = "solicitud guardada"
				};
			}
			catch (System.Exception ex)
			{
				return new ResponseService(500, ex.Message);
			}

		}



		public static bool ValidarPagos(Tbl_Pago pago, List<Detalle_Pago> pagosSeleccionados, List<Tbl_Pago> pagosPendientes)
		{
			DateTime fechaPagoSeleccionado = pago.Fecha.GetValueOrDefault();
			// Buscar todos los pagos con fecha menor a la del pago actual
			var pagosAnteriores = pagosPendientes
				.Where(p => p.Fecha.GetValueOrDefault().Month < fechaPagoSeleccionado.Month)
				.ToList();
			bool pagosAnterioresNoSeleccionados = false;
			foreach (var pagoAnterior in pagosAnteriores)
			{
				var detalleSeleccionado = pagosSeleccionados
					.FirstOrDefault(dp => dp.Pago?.Id_Pago == pagoAnterior.Id_Pago);
				if (detalleSeleccionado == null)
				{
					// No está seleccionado
					pagosAnterioresNoSeleccionados = true;
					break;
				}
				else if (detalleSeleccionado.Monto < detalleSeleccionado.Pago?.Monto_Pendiente)
				{
					// Está seleccionado pero incompleto
					pagosAnterioresNoSeleccionados = true;
					break;
				}
			}
			return pagosAnterioresNoSeleccionados;
		}
		public static PagosRequest? GetPagoARealizar(string? identify)
		{
			return SessionServices.Get<PagosRequest>("PagosRequest", identify);
		}

		public static InfoPagos GetSaldoPendiente(string? identify)
		{
			var user = AuthNetCore.User(identify);
			var pagos = new PagosOperation().GetPagos(new Tbl_Pago(), identify);
			if (pagos.Count == 0)
			{
				return new InfoPagos
				{
					IsInsolvente = false,
					Amount = 0,
					StringAmount = "0.00"
				};
			}
			double Amount = 0.0;
			if (pagos.Count > 0)
			{
				Amount = pagos.Sum(x => x.Monto_Pendiente).GetValueOrDefault();
			}
			MoneyEnum? moneyEnumValue = null;
			if (Amount > 0.0 && pagos.First()?.Money != null)
			{
				moneyEnumValue = pagos.First()?.Money; // Si la conversión es exitosa, asignamos el valor al moneyEnumValue
			}
			return new InfoPagos
			{
				Mes = Amount > 0.0 ? DateUtil.GetDateName(pagos.First()?.Fecha_contabilizacion, false) : null,
				Fecha = pagos?.First()?.Fecha_contabilizacion,
				IsInsolvente = pagos?.First()?.Fecha_contabilizacion < DateTime.Now && pagos?.First()?.Fecha_contabilizacion?.Month != DateTime.Now.Month,
				Amount = Amount,
				Money = moneyEnumValue,
				StringAmount = NumberUtility.ConvertToMoneyString(Amount)
			};
		}

		public static async Task<ResponseService> EjecutarPago(TPV datosDePago, string? identify)
		{
			try
			{
				var user = AuthNetCore.User(identify);
				PagosRequest? pagosRequest = GetPagoARealizar(identify);
				var responsable = Tbl_Profile.Get_Profile(user);
				if (pagosRequest == null)
				{
					return new ResponseService
					{
						status = 403,
						message = "Pago no encontrado"
					};
				}
				if (datosDePago == null
						|| datosDePago.CardNumber == null
						|| datosDePago.Cvv == null
						|| datosDePago.ExpYear == null
						|| datosDePago.ExpMonth == null)
				{
					return new ResponseService
					{
						status = 403,
						message = "Datos de la tarjeta no validos"
					};
				}

				//TODO: Ejecutar el pago Y VALIDAR CAMPOS REALES				
				datosDePago.pagosRequest = pagosRequest;
				TPVService tPVService = new TPVService();
				/*PowerTranzTpvResponse pagosAuthResponse = await tPVService.AuthenticateAsync(datosDePago);
				if (pagosAuthResponse.Errors?.Count > 0)
				{
					return new ResponseService
					{
						status = 403,
						message = string.Join(" - ", pagosAuthResponse.Errors.Select(e => e.Message).ToList())
					};
				}*/

				if (!int.TryParse(datosDePago.ExpMonth, out int mesInt) || mesInt < 1 || mesInt > 12)
				{
					return new ResponseService
					{
						status = 403,
						message = "El mes de expiración es inválido. Debe ser entre 1 y 12.",
					};
				}

				// Asignar el mes ya formateado con 2 dígitos
				datosDePago.ExpMonth = mesInt.ToString("D2");

				PowerTranzTpvResponse pagosResponse = await tPVService.SalesAsync(datosDePago);
				if (pagosResponse.Errors?.Count > 0)
				{
					return new ResponseService
					{
						status = 403,
						message = string.Join(" - ", pagosResponse.Errors.Select(e => e.Message).ToList())
					};
				}
				else
				{
					SessionServices.Set("PAGO_PROCESO_SERVICE", tPVService, pagosResponse.SpiToken);
					SessionServices.Set("PAGO_PROCESO_REQUEST", pagosRequest, pagosResponse.SpiToken);
					SessionServices.Set("PAGO_PROCESO_USER", user, pagosResponse.SpiToken);
					SessionServices.Set("PAGO_PROCESO_RESPONSE", pagosResponse, identify);

					//guardado de pago previo en sql server, se guarda en  estado pendiente para luego marcarlo como autorizado en el metodo autorizarPago()

					//pagosRequest!.Referencia = pagosResponseAutorizarPago?.TransactionIdentifier;
					pagosRequest!.Fecha = DateTime.Now;
					pagosRequest!.Estado = PagosState.PENDIENTE;
					pagosRequest!.Responsable_Id = responsable.Pariente_id;
					pagosRequest!.Id_User = user.UserId;
					pagosRequest!.Monto = pagosRequest!.Detalle_Pago!.Sum(x => x.Total);
					pagosRequest!.Creador = user.UserData?.Descripcion;
					pagosRequest!.TasaCambio = PageConfig.GetTasaCambio(pagosRequest!.Moneda);
					pagosRequest!.Descripcion += $"pago de {pagosRequest!.Monto} {pagosRequest!.Moneda}  {String.Join(", ", pagosRequest!.Detalle_Pago.Select(x => x.Pago?.Concepto))}";
					pagosRequest!.TpvInfo = pagosResponse;
					pagosRequest!.CardNumber = datosDePago.CardNumber.Substring(datosDePago.CardNumber.Length - 4);

					pagosRequest?.Detalle_Pago!.ForEach(detalle =>
					{
						//detalle.Pago!.Monto_Pendiente -= detalle.Monto;
						if (detalle.Pago!.Monto_Pendiente <= 0)
						{
							/*detalle.Pago!.Monto_Pendiente = 0;
							detalle.Pago!.Estado = PagosState.CANCELADO.ToString();*/
							pagosRequest!.Descripcion += $", pago de : {detalle.Pago.Concepto}";
						}
						else
						{
							pagosRequest!.Descripcion += ", pago parcial de :" + detalle.Pago.Concepto;
						}
						detalle.Fecha = DateTime.Now;
						//detalle.Pago!.Monto_Pagado += detalle.Monto;
						var updateResponse = detalle.Pago.Update();
						if (updateResponse.status != 200)
						{
							throw new Exception($"Error al actualizar el pago: {updateResponse.status} - {updateResponse.message}");
						}
					});

					pagosRequest?.Save();
					SessionServices.Set("PAGO_PROCESO_REQUEST", pagosRequest, pagosResponse.SpiToken);

					return new ResponseService
					{
						status = 200,
						message = "PAGO_PROCESO"
					};
				}

			}
			catch (Exception e)
			{
				LoggerServices.AddMessageError("ERROR: ejecutando pago", e);
				return new ResponseService
				{
					status = 500,
					message = "Error inesperado, verifique si el cargo no ha sido cargado en su tarjeta antes de intentarlo nuevamente."
				};
			}

		}
		public static async Task<ResponseService> AutorizarPago(string? identify, PT3DSResponse PT3DSResponse)
		{
			try
			{
				TPVService? tPVService = SessionServices.Get<TPVService>("PAGO_PROCESO_SERVICE", PT3DSResponse.SpiToken);
				PagosRequest? pagosRequest = SessionServices.Get<PagosRequest>("PAGO_PROCESO_REQUEST", PT3DSResponse.SpiToken);
				UserModel? user = SessionServices.Get<UserModel>("PAGO_PROCESO_USER", PT3DSResponse.SpiToken);

				PowerTranzTpvResponse pagosResponseAutorizarPago = await tPVService!.PaymentAsync(PT3DSResponse.SpiToken);

				var result = System.Text.Json.JsonSerializer.Serialize(pagosResponseAutorizarPago);
				//LoggerServices.AddMessageInfo("pagosResponseAutorizarPago result: " + result);

				if (pagosResponseAutorizarPago != null && pagosResponseAutorizarPago.Errors != null && pagosResponseAutorizarPago.Errors?.Count > 0)
				{
					return new ResponseService
					{
						status = 403,
						message = string.Join(" - ", pagosResponseAutorizarPago.Errors.Select(e => e.Message).ToList())
					};
				}
				else if (pagosResponseAutorizarPago?.Approved == false)
				{
					var pagoPendiente = new PagosRequest()
					{
						Id_Pago_Request = pagosRequest?.Id_Pago_Request
					}.Find<PagosRequest>();
					pagoPendiente!.TpvInfo = pagosResponseAutorizarPago;
					pagoPendiente.Update();
					return new ResponseService
					{
						status = 403,
						message = pagosResponseAutorizarPago?.ResponseMessage
					};
				}
				else
				{
					var responsable = Tbl_Profile.Get_Profile(user);

					var pagoPendiente = new PagosRequest()
					{
						Id_Pago_Request = pagosRequest?.Id_Pago_Request
					}.Find<PagosRequest>();


					pagoPendiente!.Referencia = pagosResponseAutorizarPago?.TransactionIdentifier;
					pagoPendiente!.Estado = PagosState.PAGADO;
					pagoPendiente!.TpvInfo = pagosResponseAutorizarPago;

					pagoPendiente?.Detalle_Pago!.ForEach(detalle =>
					{
						detalle.Pago!.Monto_Pendiente -= detalle.Monto;
						if (detalle.Pago!.Monto_Pendiente <= 0)
						{
							detalle.Pago!.Monto_Pendiente = 0;
							detalle.Pago!.Estado = PagosState.CANCELADO;
						}

						//detalle.Fecha = DateTime.Now;
						detalle.Pago!.Monto_Pagado += detalle.Monto;
						var updateResponse = detalle.Pago.Update();
						if (updateResponse.status != 200)
						{
							throw new Exception("Error al actualizar el pago");
						}
					});

					pagoPendiente?.Update();

					return new ResponseService
					{
						status = 200,
						message = "Pago realizado",
						body = PagosTemplate.GenerarFacturaHtml(pagosRequest)
					};
				}
			}
			catch (Exception ex)
			{
				return new ResponseService
				{
					status = 500,
					message = "Error durante la venta: " + ex.Message
				};
			}
		}


		public List<PagosRequest> GetManagePagos(PagosRequest inst, string? identify)
		{
			inst.orderData = [OrdeData.Asc("Fecha")];
			return inst.Where<PagosRequest>(
			 	FilterData.Equal("Estado", PagosState.PAGADO)
			);
		}

		public List<PagosRequest> GetManagePagosNoRealizados(PagosRequest inst, string? identify)
		{
			inst.orderData = [OrdeData.Asc("Fecha")];
			return inst.Where<PagosRequest>(
			 	FilterData.Equal("Estado", PagosState.PENDIENTE)
			);
		}
	}


	public class InfoPagos
	{
		public Double? Amount { get; set; }
		public string? StringAmount { get; set; }
		public MoneyEnum? Money { get; set; }
		public string? Mes { get; internal set; }
		public DateTime? Fecha { get; internal set; }
		public bool IsInsolvente { get; internal set; }
	}
}