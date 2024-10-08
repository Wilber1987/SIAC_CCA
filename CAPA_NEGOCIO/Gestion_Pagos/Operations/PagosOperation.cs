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
        public static List<Pago> GetPagos(Pago pago, string identify)
        {
            return [new Pago {
                Id_Pago = 1,
                Estudiante_Id = 101,
                Responsable_Id = 201,
                Monto = 500.75,
                Periodo_lectivo = "2024",
                Mes = "Enero",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 1, 5),
                Fecha_Limite = new DateTime(2024, 1, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juana", Segundo_nombre = "Maria", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 5,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Daniel", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 6,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Mario", Segundo_nombre = "Jose", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 7,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Maria", Segundo_nombre = "Masiel", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 8,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 9,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Julisa", Segundo_nombre = "Marisol", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            }];
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
            var pagos = GetPagos(new Pago(), identify);
            var Amount = 0.0;
            if (pagos.Count > 0 )
            {
                Amount = pagos.Sum(x => x.Monto).GetValueOrDefault();                
            }
            return new InfoPagos {
                Mes = pagos.First()?.Mes,
                Amount = Amount,
                Money = pagos.First()?.Money,
                StringAmount = NumberUtility.ConvertToMoneyString(Amount)
            };
        }

        public static ResponseService EjecutarPago(TPV datosDePago, string? identify)
        {
            try
            {
                var user = AuthNetCore.User(identify);
                PagosRequest? pagosRequest = GetPagoARealizar(identify);
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
                pagosRequest!.Id_User = user.UserId;
                pagosRequest!.Monto = pagosRequest!.Pagos!.Sum(x => x.Monto);
                pagosRequest!.Creador = user.UserData?.Descripcion;
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
        public MoneyEnum? Money { get;  set; }
        public string? Mes { get; internal set; }
    }
}