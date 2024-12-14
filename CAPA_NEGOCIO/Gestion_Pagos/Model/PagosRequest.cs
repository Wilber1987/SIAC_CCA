using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
	public class PagosRequest: EntityClass
	{
	   
		[PrimaryKey(Identity = true)]
		public int? Id_Pago_Request { get; set; }
		public string? Referencia { get; set; } 
		public string? Descripcion { get; set; } 
		public int? Responsable_Id { get; set; } 
		public int? Id_User { get; set; }
		public DateTime? Fecha { get; set; } 
		public string? Creador { get; set; }
		public  string? Estado { get;  set; }
		public double? Monto { get; set; }
		public string? Moneda { get;  set; }
		[OneToMany(TableName = "Detalle_Pago", KeyColumn = "Id_Pago_Request", ForeignKeyColumn = "Id_Pago_Request")]
		public List<Detalle_Pago>? Detalle_Pago { get; set; }

		public static List<PagosRequest> GetPagosRealizados(PagosRequest inst, string? identify)
		{
			var responsable = Tbl_Profile.Get_Profile(AuthNetCore.User(identify));
			if (inst.Responsable_Id == null)
			{
				return [];
			}
			return inst.Where<PagosRequest>(
				FilterData.Equal("Responsable_Id", responsable.Pariente_id)
			);
		}
	}
	public class Detalle_Pago : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id_Detalle { get; set; }

		public int? Id_Pago { get; set; }
		public double? Total { get; set; }
		public double? Cantidad { get; set; }
		public double? Monto { get; set; }		
		public double? Impuesto { get; set; }
		public string? Concepto { get; set; }
		public int? Id_Pago_Request { get; set; }
		[JsonProp]
		public Tbl_Pago? Estado_Anterior_Pago { get; set; }

		[ManyToOne(TableName = "PagosRequest", KeyColumn = "Id_Pago_Request", ForeignKeyColumn = "Id_Pago_Request")]
		public PagosRequest? PagosRequest { get; set; }
		
		[ManyToOne(TableName = "Tbl_Pago", KeyColumn = "Id_Pago", ForeignKeyColumn = "Id_Pago")]
		public Tbl_Pago? Pago { get; set; }
	}	

	public class TPV
	{
		public string? CardholderName { get; set; }
		public string? CardNumber { get; set; }
		public string? Cvv { get; set; }
		public int? ExpMonth { get; set; }
		public int? ExpYear { get; set; }
		public PagosRequest? pagosRequest { get; set; }
	}
}