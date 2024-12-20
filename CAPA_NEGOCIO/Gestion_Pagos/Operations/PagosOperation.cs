using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
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
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes());
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));
			var pagosP = new Tbl_Pago()
			{
				orderData = [OrdeData.Asc("Fecha")]
			}.Where<Tbl_Pago>(
				FilterData.In("Id_Estudiante", estudiantes.Select(x => x.Id).ToArray()),
				FilterData.In("Estado", PagosState.PENDIENTE.ToString())
			);
			//List<Pagos_alumnos_view> recientraidos = null;
			List<Pagos_alumnos_view> recientraidos;


			using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
			{
				siacSshClient.Connect();
				var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
				siacTunnel.Start();

				var pagosAlumnosView = new Pagos_alumnos_view();
				pagosAlumnosView.SetConnection(MySqlConnections.BellacomTest);

				recientraidos = pagosAlumnosView.Where<Pagos_alumnos_view>(
					FilterData.ISNull("fecha_anulacion"),
					FilterData.In("codigo_estudiante", estudiantes.Select(x => x.Codigo).ToArray()),
					//,
					FilterData.Greater("importe_saldo_md", 0)
				).ToList();

				siacTunnel.Stop();
				siacSshClient.Disconnect();
			}
			var recienTraidosPagos = recientraidos.SelectMany(x =>
			{
				return buildCuentasPorCobrar(x, responsable);
			}).ToList();

			return new Tbl_Pago
			{
				orderData = [OrdeData.Asc("Fecha")]
			}.Where<Tbl_Pago>(
				FilterData.In("Id_Estudiante", estudiantes.Select(x => x.Id).ToArray()),
				FilterData.ISNull("Fecha_anulacion"),
				FilterData.Greater("Monto_Pendiente", 0)
			);

		}



		private static List<Tbl_Pago> buildCuentasPorCobrar(Pagos_alumnos_view x, Tbl_Profile responsable)
		{
			MoneyEnum? moneyEnumValue = MoneyEnum.DOLARES;
			if (x?.Texto_corto != null)
			{
				Enum.TryParse(x?.Texto_corto, out MoneyEnum result);
				moneyEnumValue = result;
			}
			List<Tbl_Pago> pagos = new List<Tbl_Pago>
			{

				new Tbl_Pago {
					Estudiante_Id = x.Id_estudiante,
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
			//
			var user = AuthNetCore.User(identify);
			var pagos = new PagosOperation().GetPagos(new Tbl_Pago(), identify);
			double Amount = 0.0;
			if (pagos.Count > 0)
			{
				Amount = pagos.Sum(x => x.Monto_Pagado).GetValueOrDefault();
			}
			MoneyEnum? moneyEnumValue = null;
			if (Amount > 0.0 && pagos.First()?.Texto_corto != null)
			{
				Enum.TryParse(pagos.First()?.Texto_corto, out MoneyEnum result);
				moneyEnumValue = result; // Si la conversión es exitosa, asignamos el valor al moneyEnumValue
			}
			return new InfoPagos
			{
				Mes = Amount > 0.0 ? DateUtil.GetMonthName(pagos.First()?.Fecha_contabilizacion) : null,
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
				var pagosAuthResponse = await TPVService.AuthenticateAsync(datosDePago);
				var pagosResponse = await TPVService.SalesAsync(datosDePago);
							
				
				pagosRequest!.Referencia = Guid.NewGuid().ToString();
				pagosRequest!.Fecha = DateTime.Now;
				pagosRequest!.Estado = PagosState.PAGADO.ToString();
				pagosRequest!.Responsable_Id = responsable.Pariente_id;
				pagosRequest!.Id_User = user.UserId;
				pagosRequest!.Monto = pagosRequest!.Detalle_Pago!.Sum(x => x.Total);
				pagosRequest!.Creador = user.UserData?.Descripcion;
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
					detalle.Pago!.Monto_Pagado = detalle.Pago.Monto_Pagado + detalle.Monto;
					detalle.Pago?.Update();
				});
				//pagosRequest!.Descripcion = $"pago de {pagosRequest!.Monto} {pagosRequest!.Moneda} por los estudiantes: {String.Join(", ", pagosRequest!.Pagos!.Select(x => x.Estudiante?.Nombre_completo))}";
				pagosRequest?.Save();
				return new ResponseService
				{
					status = 200,
					message = "Pago realizado"
				};
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
	}

	public class InfoPagos
	{
		public Double? Amount { get; set; }
		public string? StringAmount { get; set; }
		public MoneyEnum? Money { get; set; }
		public string? Mes { get; internal set; }
	}
}