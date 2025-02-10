using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using DataBaseModel;
using DataBaseModel.SimpleModel;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
	public class Tbl_Pago : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id_Pago { get; set; }
		public int? Estudiante_Id { get; set; }//codigo del estudiante
		public int? Responsable_Id { get; set; }
		public double? Monto { get; set; }
		public double? Monto_Pagado { get; set; }
		public double? Monto_Pendiente { get; set; }
		public string? Periodo_lectivo { get; set; }
		public string? Documento { get; set; }
		public string? Concepto { get; set; }
		public string? Mes { get; set; }
		public MoneyEnum? Money { get; set; }
		public DateTime? Fecha_Pago { get; set; }
		public DateTime? Fecha_Limite { get; set; }
		public DateTime? Fecha { get; set; }
		public string? Estado { get; set; }
		
		[ManyToOne(TableName = "Estudiante_View", KeyColumn  = "Codigo", ForeignKeyColumn = "Estudiante_Id", isView = true)]
		public Estudiante_View? Estudiante { get; set; }

		//nuevas propiedades luego del alter 23
		public int? Id_plazo { get; set; }
		public int? Anio { get; set; }
		public int? Id_documento_cc { get; set; }
		public int? Id_documento { get; set; }
		public int? Id_clase_documento { get; set; }
		public DateTime? Fecha_documento { get; set; }
		public DateTime? Fecha_contabilizacion { get; set; }
		public int? Ejercicio { get; set; }
		public int? Periodo { get; set; }
		public int? No_documento { get; set; }
		public int? Id_deudor { get; set; }
		public string? Asignacion { get; set; }
		public string? Texto_posicion { get; set; }
		public int? Id_cuenta { get; set; }
		public int? Id_indicador_impuesto { get; set; }
		public int? Id_documento_detalle { get; set; }
		public DateTime? Fecha_anulacion { get; set; }
		public string? Usuario_anulacion { get; set; }
		public string? Texto_corto { get; set; }
		public string? Simbolo { get; set; }
	}

	public enum PagosState
	{
		PENDIENTE,
		PAGADO,
		PAGO_PARCIAL,
		CANCELADO
	}
	public enum MoneyEnum
	{
		CORDOBAS,
		DOLARES
	}
}