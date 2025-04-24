using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPCORE;
using CAPA_DATOS;

namespace CAPA_NEGOCIO.Gestion_Cursos.Model.QueryModel
{
    public class MateriasByClassQuery : QueryClass
    {
        public int? Asignatura_id { get; set; }
        public int? Clase_id { get; set; }
        public int? Estudiante_id { get; set; }
        public string? Nombre { get; set; }
        public int? Orden { get; set; }
        public int? Seccion_id { get; set; }
        public int? Materia_id { get; set; }
        public bool Repitente { get;  set; }

        public override List<MateriasByClassQuery> Get<MateriasByClassQuery>()
        {
            var dt = this.MDataMapper?.GDatos.TraerDatosSQL(GetQuery()) ;
			if (dt != null && dt.Rows.Count > 0) 
			{
				return AdapterUtil.ConvertDataTable<MateriasByClassQuery>(dt, this);
			} else 
			{
				return [];	
			}
        }

        public override string GetQuery()
        {
            if (Clase_id == null)
			{
				throw new ArgumentNullException($"Traking number not found is null");
			}
			return $@"SELECT 
			    m.asignatura_id ,
			    m.clase_id ,
			    a.nombre, 
			    ec.estudiante_id,
			    ec.repitente,
			    ec.seccion_id,
			    a.orden,
                m.id as materia_id
			    from materias m 
                inner join asignaturas a on a.id = m.asignatura_id 
                inner join estudiante_clases ec on m.clase_id = ec.clase_id 
                where m.clase_id = {Clase_id} and ec.estudiante_id  = {Estudiante_id} 
                ORDER by a.orden asc";
        }
    }
}