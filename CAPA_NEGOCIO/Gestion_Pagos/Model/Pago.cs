using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using DataBaseModel;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
    public class Pago : EntityClass
    {
        public int? Id_Pago { get; set; }
        public int? Estudiante_Id { get; set; }
        public int? Responsable_Id { get; set; }
        public double? Monto { get; set; }
        public string? Periodo_lectivo { get; set; }
        public string? Mes { get; set; }
        public MoneyEnum? Money { get; set; }
        public DateTime? Fecha_Pago { get; set; }
        public DateTime? Fecha_Limite { get; set; }
        public DateTime? Fecha { get; set; }
        public PagosState? Estado { get; set; }
        public Estudiantes? Estudiante { get; set; }
    }

    public enum PagosState
    {
        PENDIENTE,
        PAGADO
    }
    public enum MoneyEnum
    {
        CORDOBAS,
        DOLARES
    }
}