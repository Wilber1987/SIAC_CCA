using APPCORE;
using APPCORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Estudiante_clases : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public int? Estudiante_id { get; set; }
        public int? Periodo_lectivo_id { get; set; }
        public int? Clase_id { get; set; }
        public int? Seccion_id { get; set; }
        public DateTime? Transferido { get; set; }
        public DateTime? Retirado { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public Double? Promedio { get; set; }
        public bool? Repitente { get; set; }
        public int? Reprobadas { get; set; }

        [ManyToOne(TableName = "Clases", KeyColumn = "Id", ForeignKeyColumn = "Clase_id")]
        public Clases? Clases { get; set; }

        [ManyToOne(TableName = "Estudiantes", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_id")]
        public Estudiantes? Estudiantes { get; set; }

        [ManyToOne(TableName = "Periodo_lectivos", KeyColumn = "Id", ForeignKeyColumn = "Periodo_lectivo_id")]
        public Periodo_lectivos? Periodo_lectivos { get; set; }

        [ManyToOne(TableName = "Secciones", KeyColumn = "Id", ForeignKeyColumn = "Seccion_id")]
        public Secciones? Secciones { get; set; }

        //[OneToMany(TableName = "Calificaciones", KeyColumn = "Id", ForeignKeyColumn = "Estudiante_clase_id")]
        //public List<Calificaciones>? Calificaciones { get; set; }       
        public string? Descripcion
        {
            get { return $"{NumberUtility.ObtenerEnumeracion((GetNivelName()?.ToString() == "SECUNDARIA" ?  this.Clases?.Grado + 6: this.Clases?.Grado) ?? 0) } {GetNivelName()}"; }
        }

        private NivelesEnum? GetNivelName()
        {
            return Enum.IsDefined(typeof(NivelesEnum),  (this.Clases?.Nivel_id - 1) ?? 0) ?  (NivelesEnum?)this.Clases?.Nivel_id - 1: null;
        }

        public Object? Informe
        {
            get
            {
                //Calificaciones?.ForEach(C => C = new Calificaciones{Id = C.Id}.Find<Calificaciones>() ?? C);
                return null; /* Calificaciones?.GroupBy(C => C.Materia_id)
                   .Select(C => new
                   {
                       Name = C.First().Materia?.Asignaturas?.Nombre,
                       Calificaciones = C.Select(Calificacion =>
                       {
                           return new
                           {
                               Resultado = Calificacion.Resultado,
                               Evaluacion = Calificacion.Evaluacion_id
                           };
                       })
                   }).ToList();
                //.Select(g => g.Key)
                //return Calificaciones?.GroupBy(x => x.Materia)
                   // .Where(g => g.Count() == 1)
                    //.Select(g => g.Key).ToList();*/
            }
        }
        
        public List<Estudiantes> GetEstudianBySectionClass()
        {
            return Get<Estudiante_clases>().Select(c=> c.Estudiantes ?? new Estudiantes()).ToList();
        }

        public ResponseService ExportClaseBoletin()
        {
            throw new NotImplementedException();
        }
       

        public Estudiante_clases? FindEstudiante()
        {
            throw new NotImplementedException();
        }

    }
    public enum NivelesEnum {
       SECUNDARIA,  PRIMARIA,  PREESCOLAR
    }
}
