using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.Util;
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
		public int? Idtestudiante { get; set; }
		public int? Idbellacom { get; set; }
		public string? Primer_nombre { get; set; }
		public string? Segundo_nombre { get; set; }
		public string? Primer_apellido { get; set; }
		public string? Segundo_apellido { get; set; }
		public DateTime? Fecha_nacimiento { get; set; }
		public string? Lugar_nacimiento { get; set; }
		public string? Sexo { get; set; }
		public string? Direccion { get; set; }
		public string? Codigo { get; set; }
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
		public int? Id_familia { get; set; }
		public String? Vivecon { get; set; }
		public int? Id_religion { get; set; }
		public int? Id_pais { get; set; }
		public int? Id_region { get; set; }
		public int? EgresoExAlumno { get; set; }
		public DateTime? Fecha_ingreso { get; set; }
		public int? Id_cliente { get; set; }
		public string? Codigomed { get; set; }
		public Double? Saldoeamd { get; set; }
		public string? Sacramento { get; set; }
		public int? Aniosacra { get; set; }
		public string? Colegio_procede { get; set; }

		public bool? Activo { get; set; }
		public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }

		[OneToMany(TableName = "Estudiante_clases", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiante_clases>? Estudiante_clases { get; set; }

		[OneToMany(TableName = "Estudiantes_responsables_familia", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		public List<Estudiantes_responsables_familia>? Responsables { get; set; }

		[ManyToOne(TableName = "religiones", KeyColumn = "id", ForeignKeyColumn = "Id_religion")]
		public Religiones? Religion { get; set; }

		[ManyToOne(TableName = "paises", KeyColumn = "Id_pais", ForeignKeyColumn = "Id_pais")]
		public Paises? Pais { get; set; }

		[ManyToOne(TableName = "regiones", KeyColumn = "Id_region", ForeignKeyColumn = "Id_region")]
		public Regiones? Region { get; set; }

		//[OneToMany(TableName = "Estudiante_Clases_View", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
		//public List<Estudiante_Clases_View>? Clases { get; set; }

		public Estudiantes? FindEstudiante(string? identity)
		{

			if (AuthNetCore.HavePermission(identity, Permissions.GESTION_ESTUDIANTES))
			{
				return GetFullEstudiante();
			}
			else if (AuthNetCore.HavePermission(identity, Permissions.GESTION_ESTUDIANTES_PROPIOS))
			{
				UserModel user = AuthNetCore.User(identity);
				Parientes? pariente = new Parientes { User_id = user.UserId }.Find<Parientes>();
				if (pariente?.Estudiantes_responsables_familia?.Find(r => r.Estudiante_id == Id) != null)
				{
					return GetFullEstudiante(false);
				}
				else
				{
					throw new Exception("Estudiante no esta asignado a este usuario");
				}
			}
			throw new Exception("No posee permisos");
		}

		public Estudiantes GetFullEstudiante(bool getFullData = true)
		{
			var estudiante = Find<Estudiantes>();
			if (estudiante != null)
			{
				estudiante.Responsables = new Estudiantes_responsables_familia { Estudiante_id = estudiante.Id }
					.Get<Estudiantes_responsables_familia>();
				if (!getFullData)
				{
					var PeriodoActivo = Periodo_lectivos.PeriodoActivo();
					estudiante.Estudiante_clases = estudiante.Estudiante_clases
						.Where(clase => clase.Periodo_lectivo_id == Periodo_lectivos.PeriodoActivo()?.Id).ToList();
				}
				return estudiante;
			}
			else
			{
				throw new Exception("Estudiante no existe");
			}
		}
		public List<Clase_Group>? Clase_Group { get; set; }
		public ResponseService UpdateOwEstudiantes(string? v)
		{
			throw new NotImplementedException();
		}

	}

	public class ViewEstudiantesActivosSiac : Estudiantes
	{
		public object? CreateViewEstudiantesActivos()
		{
			int currentYear = MigrationDates.GetCurrentYear();

			string query = $"DROP VIEW IF EXISTS viewestudiantesactivossiac; " +
							$"CREATE VIEW viewestudiantesactivossiac AS " +
							$"SELECT DISTINCT e.* " +
							$"FROM estudiantes e " +
							$"INNER JOIN estudiante_clases ec ON ec.estudiante_id = e.id " +
							// $"WHERE ec.periodo_lectivo_id = (SELECT id FROM periodo_lectivos pl WHERE pl.nombre_corto IN ('2023','2024')) " +
							$"ORDER BY e.id DESC;";



			return ExecuteSqlQuery(query);
		}

		public object? DestroyView(String view)
		{
			string query = $"DROP VIEW IF EXISTS {view};";
			return ExecuteSqlQuery(query);
		}
	}

	public class Calificacion_Group
	{
		public int? Id { get; set; }
		public double? Resultado { get; set; }
		public string? Evaluacion { get; set; }
		public string? Tipo { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Periodo { get; set; }
		public int? Order { get; set; }
		public string? EvaluacionCompleta { get; set; }
		public string? Observaciones { get; internal set; }
	}

	public class Asignatura_Group
	{
		public string? Descripcion { get; set; }
		public List<string?>? Evaluaciones { get; set; }
		public List<Calificacion_Group>? Calificaciones { get; set; }
		public string? Docente { get; set; }
		public string? Descripcion_Corta { get; set; }
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
		public List<Estudiante_Group>? Estudiantes { get; set; }
	}

	public class Estudiante_Group
	{
		public string? Descripcion { get; set; }
		public List<string?>? Evaluaciones { get; set; }
		public List<Calificacion_Group>? Calificaciones { get; set; }
		public List<Asignatura_Group>? Asignaturas { get; internal set; }
		public string? Estado { get; set; }
		public string? Sexo { get; set; }
	}
}
