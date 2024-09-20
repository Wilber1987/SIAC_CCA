using API.Controllers;
using CAPA_DATOS;
using CAPA_NEGOCIO.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
	public class Parientes : EntityClass
	{
		#region Propiertys
		[PrimaryKey(Identity = true)]
		public int? Id { get; set; }
		public string? Primer_nombre { get; set; }
		public string? Segundo_nombre { get; set; }
		public string? Primer_apellido { get; set; }
		public string? Segundo_apellido { get; set; }
		public string? Sexo { get; set; }
		public string? Profesion { get; set; }
		public string? Direccion { get; set; }
		public string? Lugar_trabajo { get; set; }
		public string? Telefono { get; set; }
		public string? Celular { get; set; }
		public string? Foto { get; set; }
		public string? Telefono_trabajo { get; set; }
		public string? Email { get; set; }
		public int? Estado_civil_id { get; set; }
		public int? Religion_id { get; set; }
		public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }
		public int? Id_Titulo { get; set; }
		public int? Id_Region { get; set; }
		public int? Id_Estado_Civil { get; set; }
		public bool? Responsable_Pago { get; set; }
		public String? Ex_Alumno { get; set; }
		public DateTime? Fecha_Nacimiento { get; set; }
		public DateTime? Created_at { get; set; }// es fecha grabacion
		public DateTime? Fecha_Modificacion { get; set; }// es fecha grabacion
		public String? Usuario_Grabacion { get; set; }
		public String? Usuario_Edicion { get; set; }
		public Double? Ejercicio { get; set; }
		public bool? Actualizado { get; set; }
		public int? No_Responsable { get; set; }
		public int? Id_familia { get; set; }	
       	public int? Id_relacion_familiar { get; set; }
		public int? User_id { get; set; }
		
		[JsonProp]
		public List<ProfileRequest>? ProfileRequest { get; internal set; }

		// [ManyToOne(TableName = "Security_Users", KeyColumn = "Id_User", ForeignKeyColumn = "User_id")]

		[OneToMany(TableName = "Estudiantes_responsables_familia", KeyColumn = "Id", ForeignKeyColumn = "Pariente_id")]
		public List<Estudiantes_responsables_familia>? Estudiantes_responsables_familia { get; set; }
        
		
        #endregion

        public static List<Estudiantes> GetOwEstudiantes(string? identity, Estudiantes estudiante)
		{
			UserModel user = AuthNetCore.User(identity);
			Parientes? pariente = new Parientes{User_id = user.UserId}.Find<Parientes>();
			if (pariente?.Estudiantes_responsables_familia != null)
			{
				return estudiante.Where<Estudiantes>(
					FilterData.In("Id",	pariente.Estudiantes_responsables_familia?.Select(r => r.Estudiante_id).ToArray())
				);
			}
			//throw new Exception("No posee estudiantes asociados");
			return [];
		}
	}
}
