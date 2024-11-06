using System;
using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Pagos.Model
{
    public class Viewestudiantesboletas : EntityClass
    {
        public int? IdTPeriodoAcademico { get; set; }
        public int? Ejercicio { get; set; }
        public string? Tipo { get; set; }
        public int? IdPeriodoAcademico { get; set; }
        public int? IdEstudiante { get; set; }
        public int? IdTEstudiante { get; set; }
        public int? Periodo { get; set; }
        public int? IdMoneda { get; set; }
        public string? IdTMoneda { get; set; }
        public double? ImporteMD { get; set; }
        public double? Descuento { get; set; }
        public double? ImporteDescuentoMD { get; set; }
        public double? ImporteNetoMD { get; set; }
        public string? Estatus { get; set; }
        public bool? Contabilizado { get; set; }
        public DateTime? FechaGrabacion { get; set; }
        public int? Codigo { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public int? Ciclo { get; set; }
        public string? GradoActual { get; set; }
        public string? CursoActual { get; set; }
        public string? GradoSiguiente { get; set; }
        public string? CursoSiguiente { get; set; }
    }
}
