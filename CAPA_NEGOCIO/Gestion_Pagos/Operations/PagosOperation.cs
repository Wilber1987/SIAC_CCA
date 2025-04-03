using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Model.PowerTranzTpv;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
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
				FilterData.ISNull("Fecha_anulacion"),
				FilterData.Greater("Monto_Pendiente", 0)
			);

		}

		public List<Estudiantes> UpdatePagosFromBellacon(string identify)
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

		public Object GetPagosAllPagos(Tbl_Pago pago, string identify)
		{
			//return new List<Tbl_Pago>();
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes(), true);
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
		private static List<Tbl_Pago> BuildCuentasPorCobrar(Pagos_alumnos_view x, Tbl_Profile responsable)
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
					Estado = PagosState.PENDIENTE.ToString(),
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


		public static ResponseService SetPagosRequest(PagosRequest inst, string? identify)
		{
			inst.Monto = inst.Detalle_Pago!.Sum(x => x.Total);
			inst.Moneda = inst.Detalle_Pago.First().Pago!.Money.ToString();

			SeasonServices.Set("PagosRequest", inst, identify);
			return new ResponseService
			{
				status = 200,
				message = "solicitud guardada"
			};
		}
		public static PagosRequest? GetPagoARealizar(string? identify)
		{
			return SeasonServices.Get<PagosRequest>("PagosRequest", identify);
		}

		public static InfoPagos GetSaldoPendiente(string? identify)
		{
			var user = AuthNetCore.User(identify);
			var pagos = new PagosOperation().GetPagos(new Tbl_Pago(), identify);
			if (pagos.Count == 0)
			{
				return new InfoPagos
				{
				    IsInsolvente= false,
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
					SeasonServices.Set("PAGO_PROCESO_SERVICE", tPVService, pagosResponse.SpiToken);
					SeasonServices.Set("PAGO_PROCESO_REQUEST", pagosRequest, pagosResponse.SpiToken);
					SeasonServices.Set("PAGO_PROCESO_USER", user, pagosResponse.SpiToken);

					SeasonServices.Set("PAGO_PROCESO_RESPONSE", pagosResponse, identify);
					return new ResponseService
					{
						status = 200,
						message = "PAGO_PROCESO"
					};
				}
			}
			catch (System.Exception e)
			{
				return new ResponseService
				{
					status = 500,
					message = e.Message
				};
			}

		}
		public static async Task<ResponseService> AutorizarPago(string? identify, PT3DSResponse PT3DSResponse)
		{
			TPVService? tPVService = SeasonServices.Get<TPVService>("PAGO_PROCESO_SERVICE", PT3DSResponse.SpiToken);
			PagosRequest? pagosRequest = SeasonServices.Get<PagosRequest>("PAGO_PROCESO_REQUEST", PT3DSResponse.SpiToken);
			UserModel? user = SeasonServices.Get<UserModel>("PAGO_PROCESO_USER", PT3DSResponse.SpiToken);

			//PowerTranzTpvResponse? pagosResponse = SeasonServices.Get<PowerTranzTpvResponse>("PAGO_PROCESO_RESPONSE",  PT3DSResponse.SpiToken);
			PowerTranzTpvResponse pagosResponseAutorizarPago = await tPVService!.PaymentAsync(PT3DSResponse.SpiToken);
			if (pagosResponseAutorizarPago != null && pagosResponseAutorizarPago.Errors?.Count > 0)
			{
				return new ResponseService
				{
					status = 403,
					message = string.Join(" - ", pagosResponseAutorizarPago.Errors.Select(e => e.Message).ToList())
				};
			}
			else
			{
				//var user = AuthNetCore.User(identify);
				var responsable = Tbl_Profile.Get_Profile(user);

				pagosRequest!.Referencia = pagosResponseAutorizarPago?.TransactionIdentifier;
				pagosRequest!.Fecha = DateTime.Now;
				pagosRequest!.Estado = PagosState.PAGADO.ToString();
				pagosRequest!.Responsable_Id = responsable.Pariente_id;
				pagosRequest!.Id_User = user.UserId;
				pagosRequest!.Monto = pagosRequest!.Detalle_Pago!.Sum(x => x.Total);
				pagosRequest!.Creador = user.UserData?.Descripcion;
				pagosRequest!.TasaCambio = PageConfig.GetTasaCambio(pagosRequest!.Moneda);
				pagosRequest!.Descripcion = $"pago de {pagosRequest!.Monto} {pagosRequest!.Moneda} por los estudiantes: {String.Join(", ", pagosRequest!.Detalle_Pago.Select(x => x.Pago?.Concepto))}";
				pagosRequest!.TpvInfo = pagosResponseAutorizarPago;
				pagosRequest?.Detalle_Pago!.ForEach(detalle =>
				{
					detalle.Pago!.Monto_Pendiente = detalle.Pago.Monto_Pendiente - detalle.Monto;
					if (detalle.Pago!.Monto_Pendiente <= 0)
					{
						detalle.Pago!.Monto_Pendiente = 0;
						detalle.Pago!.Estado = PagosState.CANCELADO.ToString();
						pagosRequest!.Descripcion += ", pago de :" + detalle.Pago.Concepto;
					}
					else
					{
						pagosRequest!.Descripcion += ", pago parcial de :" + detalle.Pago.Concepto;
					}
					detalle.Fecha = DateTime.Now;
					detalle.Pago!.Monto_Pagado = detalle.Pago.Monto_Pagado + detalle.Monto;
					var updateResponse = detalle.Pago.Update();
					if (updateResponse.status != 200)
					{
						throw new Exception("Error al actualizar el pago");
					}
				});
				//pagosRequest!.Descripcion = $"pago de {pagosRequest!.Monto} {pagosRequest!.Moneda} por los estudiantes: {String.Join(", ", pagosRequest!.Pagos!.Select(x => x.Estudiante?.Nombre_completo))}";
				pagosRequest?.Save();
				return new ResponseService
				{
					status = 200,
					message = "Pago realizado",
					body = PagosTemplate.GenerarFacturaHtml(pagosRequest)
				};
			}
		}

		public List<PagosRequest> GetManagePagos(PagosRequest inst, string? identify)
		{
			inst.orderData = [OrdeData.Asc("Fecha")];
			return inst.Where<PagosRequest>(
			//FilterData.Equal("Responsable_Id", responsable.Pariente_id)
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