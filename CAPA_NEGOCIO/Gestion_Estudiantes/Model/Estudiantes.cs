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
		//[OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiante_clases>? Estudiante_clases { get; set; }
		//[OneToMany(TableName = "Responsables", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Responsables>? Responsables { get; set; }

		[OneToMany(TableName = "Estudiante_Clases_View", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
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

		private Estudiantes GetFullEstudiante()
		{
			var estudiante = Find<Estudiantes>();
			if (estudiante != null)
			{
				estudiante.Estudiante_clases = new Estudiante_clases { Estudiante_id = estudiante.Id }
					.Get<Estudiante_clases>();
				/*estudiante.Estudiante_clases.ForEach(c =>
				{
					c.Calificaciones = new Calificaciones { Estudiante_clase_id = c.Clase_id }.Get<Calificaciones>();
				});*/
				estudiante.Responsables = new Responsables { Estudiante_id = estudiante.Id }
					.Get<Responsables>();
				return estudiante;
			}
			else
			{
				throw new Exception("Estudiante no existe");
			}
		}
		public List<Clase_Group>? Clase_Group
		{
			get
			{
				return Clases?.Where(C => C.Nombre_nota != null).GroupBy(C => C.Descripcion)
				   .Select(C => new Clase_Group
				   {
					   Descripcion = C.First().Descripcion,
					   Asignaturas = C.GroupBy(A => A.Nombre_asignatura).Select(A =>
					   {
						   return new Asignatura_Group
						   {
							   Descripcion = A.First().Nombre_asignatura,
							   Evaluaciones = A.GroupBy(e => e.Evaluacion).Where(g => g.Count() == 1).Select(g => g.First()).Select(g => g.Evaluacion).ToList(),
							   Calificaciones = [.. A.Select(Calificacion =>
							   {
								   return new Calificacion_Group
								   {
									   Id = Calificacion.Id,
									   Resultado = Calificacion.Resultado,
									   Evaluacion = Calificacion.Evaluacion ?? "",
									   Tipo = Calificacion.Tipo,
									   Fecha = Calificacion.Fecha
								   };
							   }).OrderBy(c => c.Fecha)
							   .ThenBy(c => c.Evaluacion.Contains("B") ? 1 :
									c.Evaluacion.Contains("S") ? 2 :
									c.Evaluacion.Contains("F") ? 3 : 4) // Ordenar por Evaluacion
                               ]
						   };
					   }).ToList()
				   }).ToList();
			}
		}
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
	}

	public class Asignatura_Group
	{
		public string? Descripcion { get; set; }
		public List<string?>? Evaluaciones { get; set; }
		public List<Calificacion_Group>? Calificaciones { get; set; }
	}

	public class Clase_Group
	{
		public string? Descripcion { get; set; }
		public List<Asignatura_Group>? Asignaturas { get; set; }
	}
}
