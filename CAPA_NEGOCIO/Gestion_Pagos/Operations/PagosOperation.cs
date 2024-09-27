using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 2,
                Estudiante_Id = 102,
                Responsable_Id = 202,
                Monto = 450.00,
                Periodo_lectivo = "2024",
                Mes = "Febrero",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 2, 10),
                Fecha_Limite = new DateTime(2024, 2, 28),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 3,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
            },
            new Pago
            {
                Id_Pago = 4,
                Estudiante_Id = 103,
                Responsable_Id = 203,
                Monto = 550.25,
                Periodo_lectivo = "2024",
                Mes = "Marzo",
                Money = MoneyEnum.CORDOBAS,
                Fecha = new DateTime(2024, 3, 15),
                Fecha_Limite = new DateTime(2024, 3, 31),
                Estado = PagosState.PENDIENTE,
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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
                Estudiante = new Estudiantes { Id = 101, Primer_nombre = "Juan Perez", Segundo_nombre = "Pepe", Primer_apellido = "Perez", Segundo_apellido = "Perez" }
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

        public static ResponseService EjecutarPago(TPV datosDePago, string? identify)
        {
            try
            {
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
                pagosRequest!.Estado = PagosState.PAGADO;
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
}