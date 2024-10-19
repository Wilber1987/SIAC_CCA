using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using DataBaseModel;

namespace CAPA_NEGOCIO.Gestion_Pagos.Operations
{
	public class PagosOperation
	{

		public static List<Pagos_alumnos_view> GetPagos(Tbl_Pago pago, string identify)
		{
			var estudiantes = Parientes.GetOwEstudiantes(identify, new Estudiantes());
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));
			var pagosP = new Pagos_alumnos_view()
			{
				orderData = [OrdeData.Asc("fecha_documento"), OrdeData.Asc("nombres")]
			}.Where<Pagos_alumnos_view>(
				FilterData.In("codigo_estudiante", estudiantes.Select(x => x.Codigo).ToArray())
				//,FilterData.In("Estado", PagosState.PENDIENTE.ToString())
			);		
			
			return pagosP ?? new List<Pagos_alumnos_view>();
		}

		private static void CreateFakePayments(Estudiantes x, Tbl_Profile responsable)
		{
			List<Tbl_Pago> pagos = [
				new Tbl_Pago {
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 500.75,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Pago de viaje",
					Periodo_lectivo = "2024",
					Mes = "Enero",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 1, 5),
					Fecha_Limite = new DateTime(2024, 1, 31),
					Estado = PagosState.PENDIENTE.ToString()
				},
				new Tbl_Pago
				{
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 550.25,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Matricula",
					Periodo_lectivo = "2024",
					Mes = "Marzo",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 3, 11),
					Fecha_Limite = new DateTime(2024, 3, 31),
					Estado = PagosState.PENDIENTE.ToString()
				},
				new Tbl_Pago
				{
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 550.25,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Mensualidad",
					Periodo_lectivo = "2024",
					Mes = "Marzo",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 3, 12),
					Fecha_Limite = new DateTime(2024, 3, 31),
					Estado = PagosState.PENDIENTE.ToString()
				},
				new Tbl_Pago
				{
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 550.25,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Mensualidad",
					Periodo_lectivo = "2024",
					Mes = "Abril",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 4, 15),
					Fecha_Limite = new DateTime(2024, 3, 31),
					Estado = PagosState.PENDIENTE.ToString()
				},
				new Tbl_Pago
				{
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 550.25,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Mensualidad",
					Periodo_lectivo = "2024",
					Mes = "Mayo",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 5, 15),
					Fecha_Limite = new DateTime(2024, 3, 31),
					Estado = PagosState.PENDIENTE.ToString()
				},
				new Tbl_Pago
				{
					Estudiante_Id = x.Id,
					Responsable_Id = responsable.Pariente_id,
					Monto = 550.25,
					Monto_Pagado = 0,
					Monto_Pendiente = 500.75,
					Documento = "00001",
					Concepto = "Matricula",
					Periodo_lectivo = "2024",
					Mes = "Marzo",
					Money = MoneyEnum.CORDOBAS,
					Fecha = new DateTime(2024, 3, 15),
					Fecha_Limite = new DateTime(2024, 3, 31),
					Estado = PagosState.PENDIENTE.ToString()
				}];
			pagos.ForEach(p => p.Save());
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
			var user = AuthNetCore.User(identify);
			var pagos = GetPagos(new Tbl_Pago(), identify);
			double Amount = 0.0;
			if (pagos.Count > 0)
			{
				Amount = pagos.Sum(x => x.Monto).GetValueOrDefault();
			}
			return new InfoPagos
			{
				Mes = Amount > 0.0 ? pagos.First()?.Mes : null,
				Amount = Amount,
				Money = Amount > 0.0 ? pagos.First()?.Money : null,
				StringAmount = NumberUtility.ConvertToMoneyString(Amount)
			};
		}

		public static ResponseService EjecutarPago(TPV datosDePago, string? identify)
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
						message = "pago no encontrado"
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
						message = "datos de la tarjeta no validos"
					};
				}
				//TODO: Ejecutar el pago Y VALIDAR CAMPOS REALES
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
					message = "pago realizado"
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