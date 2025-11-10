using APPCORE;
using CAPA_NEGOCIO.UpdateModule.Model;
using DataBaseModel;

namespace CAPA_NEGOCIO.Reports.Models
{
    public class EstudiantesUpdateReportModel : QueryClass
    {
        public int? Id { get; set; }
        public string? Id_familia { get; set; }
        public string? Codigo { get; set; }
        public string? Primer_nombre { get; set; }
        public string? Segundo_nombre { get; set; }
        public string? Primer_apellido { get; set; }
        public string? Segundo_apellido { get; set; }
        public DateTime? Fecha_nacimiento { get; set; }
        public string? Periodo_Lectivo_Update { get; set; }
        public string? Grado { get; set; }
        public string? Seccion { get; set; }
        public string? Nivel { get; set; }
        public int? Periodo_lectivo_id { get; set; }
        public string? Nombre_completo { get { return $"{Primer_nombre} {Segundo_nombre} {Primer_apellido} {Segundo_apellido}"; } }


        [JsonProp]
        public SecurityOption? SecurityOption { get; set; }

        public override string GetQuery()
        {
            var filter = filterData?.Find(f => f.PropName == "Periodo_Lectivo_Update");
            if (Periodo_Lectivo_Update == null && filter == null)
            {
                var PeriodoActivo = Periodo_lectivos.PeriodoActivo();              
                Periodo_Lectivo_Update = PeriodoActivo?.Nombre_corto;
            } else if(Periodo_Lectivo_Update == null && filter != null)
            {
                 Periodo_Lectivo_Update = filter?.Values.FirstOrDefault();
            }
            var periodo = new Periodo_lectivos().Find<Periodo_lectivos>(
                FilterData.Equal("Nombre_corto", Periodo_Lectivo_Update)
            );
            Periodo_lectivo_id = periodo?.Id;
            return $@"SELECT
                e.id_familia,
                e.codigo,
                e.id,
                e.primer_nombre,
                e.segundo_nombre,
                e.primer_apellido,
                e.segundo_apellido,	
                e.fecha_nacimiento,
                e.SecurityOption,
                e.Periodo_Lectivo_Update,
                c.grado,
                s.nombre as seccion,
                n.nombre as nivel,
                ec.periodo_lectivo_id
            FROM
                update_data.estudiantes_data_update as e
            INNER JOIN estudiante_clases as ec ON
                e.id = ec.estudiante_id
            INNER JOIN clases as c ON
                c.id = ec.clase_id
            INNER JOIN secciones AS s ON
                s.id = ec.seccion_id
            INNER JOIN niveles AS n ON
                n.id = c.nivel_id
            WHERE
                e.Periodo_Lectivo_Update = '{Periodo_Lectivo_Update}'
                AND ec.periodo_lectivo_id = {Periodo_lectivo_id}
            Order by grado";
        }
    }
}