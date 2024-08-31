using API.Controllers;
using CAPA_DATOS;
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
		public int? Id_User { get; set; }
		public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }

		public DateTime? Created_at { get; set; }

		public DateTime? Updated_at { get; set; }
		[OneToMany(TableName = "Responsables", KeyColumn = "Id", ForeignKeyColumn = "Pariente_id")]

		public List<Responsables>? Responsables { get; set; }
		#endregion
		
		public static List<Estudiantes> GetOwEstudiantes(string? identity, Estudiantes estudiante)
		{
			UserModel user = AuthNetCore.User(identity);
			Parientes? pariente = new Parientes().Find<Parientes>(FilterData.Equal("email", user.mail));
			if (pariente?.Responsables != null)
			{
				return estudiante.Where<Estudiantes>(
					FilterData.In("Id",	pariente.Responsables?.Select(r => r.Estudiante_id).ToArray())
				);
			}
			throw new Exception("No posee estudiantes asociados");
		}
	}
}
