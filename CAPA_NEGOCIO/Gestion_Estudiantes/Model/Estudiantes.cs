using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
	public class Estudiantes : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id { get; set; }
		public string? Primer_nombre { get; set; }
		public string? Segundo_nombre { get; set; }
		public string? Primer_apellido { get; set; }
		public string? Segundo_apellido { get; set; }
		public DateTime? Fecha_nacimiento { get; set; }
		public string? Lugar_nacimiento { get; set; }
		public string? Sexo { get; set; }
		public string? Direccion { get; set; }
		public string? Codigo { get; set; }
		public int? Religion_id { get; set; }
		public int? Madre_id { get; set; }
		public int? Padre_id { get; set; }
		public DateTime? Created_at { get; set; }
		public DateTime? Updated_at { get; set; }
		public string? Foto { get; set; }
		public Double? Peso { get; set; }
		public Double? Altura { get; set; }
		public string? Tipo_sangre { get; set; }
		public string? Padecimientos { get; set; }
		public string? Alergias { get; set; }
		public int? Recorrido_id { get; set; }
		public bool? Activo { get; set; }
		[OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiante_clases>? Estudiante_clases { get; set; }

		[OneToMany(TableName = "Estudiantes_responsables_familias", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiantes_responsables_familias>? Estudiantes_responsables_familias { get; set; }


		//[OneToMany(TableName = "Estudiante_Clases_View", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiante_Clases_View>? Clases { get; set; }

		public Estudiantes? FindEstudiante(string? identity)
		{

			if (AuthNetCore.HavePermission(identity, Permissions.GESTION_ESTUDIANTES))
			{
				return GetFullEstudiante();
			}
			else if (AuthNetCore.HavePermission(identity, Permissions.GESTION_ESTUDIANTES_PROPIOS))
			{
				UserModel user = AuthNetCore.User(identity);
				Parientes? pariente = new Parientes().Find<Parientes>(FilterData.Equal("email", user.mail));
				if (pariente?.Responsables?.Find(r => r.Estudiante_id == Id) != null)
				{
					return GetFullEstudiante();
				}
				else
				{
					throw new Exception("Estudiante no esta asignado a este usuario");
				}
			}
			throw new Exception("No posee permisos");
		}

		public Estudiantes GetFullEstudiante()
		{
			var estudiante = Find<Estudiantes>();
			if (estudiante != null)
			{
				/*var ClasesF = new Estudiante_Clases_View { 
					Estudiante_id = estudiante.Id }.Where<Estudiante_Clases_View>(FilterData.NotNull("Nombre_nota"));
				estudiante.Clase_Group = InformeClasesBuilder.BuildClaseGroupList(ClasesF);*/

				estudiante.Responsables = new Responsables { Estudiante_id = estudiante.Id }
					.Get<Responsables>();
				return estudiante;
			}
			else
			{
				throw new Exception("Estudiante no existe");
			}
		}
		public static Clase_Group GetClaseEstudianteConsolidado(Estudiante_Clases_View estudiante_Clases_View)
		{

			var ClasesF = estudiante_Clases_View.Where<Estudiante_Clases_View>(FilterData.NotNull("Nombre_nota"));
			var clase_Group = InformeClasesBuilder.BuildClaseGroupList(ClasesF);
			return clase_Group.First();
		}
		public static Clase_Group GetClaseEstudianteCompleta(Estudiante_Clases_View estudiante_Clases_View)
		{

			var ClasesF = estudiante_Clases_View.Get<Estudiante_Clases_View>();
			var clase_Group = InformeClasesBuilder.BuildClaseGroupList(ClasesF);
			return clase_Group.First();
		}
		public List<Clase_Group>? Clase_Group { get; set; }
		public List<Estudiantes> UpdateOwEstudiantes(string? v)
		{
			throw new NotImplementedException();
		}
	}

	public class Calificacion_Group
	{
		public int? Id { get; set; }
		public double? Resultado { get; set; }
		public required string Evaluacion { get; set; }
		public string? Tipo { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Periodo { get; set; }
		public int? Order { get; set; }
	}

	public class Asignatura_Group
	{
		public string? Descripcion { get; set; }
		public List<string?>? Evaluaciones { get; set; }
		public List<Calificacion_Group>? Calificaciones { get; set; }
		public string? Docente { get; set; }
	}

	public class Clase_Group
	{
		public string? Clase { get; set; }
		public int? Id_Clase { get; set; }
		public string? Guia { get; set; }
		public string? Repite { get; set; }
		public string? Nivel { get; set; }
		public string? Seccion { get; set; }
		public List<Asignatura_Group>? Asignaturas { get; set; }

	}
}
