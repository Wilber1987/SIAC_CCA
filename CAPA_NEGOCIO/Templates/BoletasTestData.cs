using CAPA_NEGOCIO.Gestion_Pagos.Model;

namespace CAPA_NEGOCIO.Templates
{
    internal class BoletasTestData
	{
		internal static List<Viewestudiantesboletas> GetTestData()
		{
			return [
				new Viewestudiantesboletas
				{
					IdTPeriodoAcademico = null,
					Ejercicio = 2026,
					Tipo = "CARGOMATRICULA",
					IdPeriodoAcademico = 10,
					IdEstudiante = 1850,
					IdTEstudiante = 3885,
					Periodo = 1,
					IdMoneda = 1,
					IdTMoneda = "NIO",
					ImporteMD = 7691.0,
					Descuento = 0.0,
					ImporteDescuentoMD = 0.0,
					ImporteNetoMD = 7691.0,
					Estatus = "I",
					Contabilizado = false,
					FechaGrabacion = DateTime.Parse("2025-10-30"),
					Codigo = 1850,
					Nombres = "NATALIA LUCIA",
					Apellidos = "RODRIGUEZ MARTINEZ",
					Ciclo = 2025,
					Grado_Actual = "OCTAVO GRADO",
					Curso_Actual = "SECUNDARIA",
					Grado_Siguiente = null,
					Curso_Siguiente = null,
					idfamilia = null,
					idservicio = 7
				},
				new Viewestudiantesboletas
				{
					IdTPeriodoAcademico = null,
					Ejercicio = 2026,
					Tipo = "CARGOMATRICULA",
					IdPeriodoAcademico = 10,
					IdEstudiante = 1850,
					IdTEstudiante = 3885,
					Periodo = 1,
					IdMoneda = 1,
					IdTMoneda = "NIO",
					ImporteMD = 10255.0,
					Descuento = 0.0,
					ImporteDescuentoMD = 0.0,
					ImporteNetoMD = 10255.0,
					Estatus = "I",
					Contabilizado = false,
					FechaGrabacion = DateTime.Parse("2025-10-30"),
					Codigo = 1850,
					Nombres = "NATALIA LUCIA",
					Apellidos = "RODRIGUEZ MARTINEZ",
					Ciclo = 2025,
					Grado_Actual = "OCTAVO GRADO",
					Curso_Actual = "SECUNDARIA",
					Grado_Siguiente = null,
					Curso_Siguiente = null,
					idfamilia = null,
					idservicio = 39
				}
			];

		}
	}
}