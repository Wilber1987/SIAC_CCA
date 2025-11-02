using System;
using APPCORE;

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
        public string? Grado_Actual { get; set; }
        public string? Curso_Actual { get; set; }
        public string? Grado_Siguiente { get; set; }
        public string? Curso_Siguiente { get; set; }
        public int? idfamilia { get; set; }
        public int? idservicio { get; set; }


        public string GetBoletasQuery()
        {
            return @"
                SELECT 
                    cargo.*,  
                    cargo.ejercicio, cargo.tipo, cargo.idperiodoacademico, cargo.idestudiante, 
                    est.idtestudiante, 
                    cargo.periodo, cargo.idmoneda, tgm.idtmoneda,
                    cargo.importemd AS importemd,
                    cargo.descuento AS descuento,
                    cargo.importedescuentomd AS importedescuentomd,
                    cargo.importenetomd AS importenetomd,
                    cargo.estatus,
                    cargo.fechagrabacion,
                    est.idestudiante AS codigo,
                    est.nombres,
                    est.apellidos,
                    YEAR(current_date) AS ciclo,
                    nactual.grado AS grado_actual,
                    nactual.nivel AS curso_actual,
                    nsiguiente.grado AS grado_siguiente,
                    nsiguiente.nivel AS curso_siguiente
                            
                FROM tbl_aca_estudiantecargo cargo
                INNER JOIN tbl_aca_estudiante est 
                    ON est.idestudiante = cargo.idestudiante                                

                INNER JOIN (
                    SELECT 
                        matri.idestudiante, 
                        nactual.texto AS grado, 
                        areaactual.texto AS nivel
                    FROM tbl_aca_matricula matri
                    INNER JOIN tbl_aca_periodoacademico per 
                        ON per.idperiodoacademico = matri.idperiodoacademico
                    INNER JOIN tbl_aca_academianivel nactual 
                        ON nactual.idacademianivel = matri.idacademianivel 
                    INNER JOIN tbl_aca_academiaarea areaactual 
                        ON areaactual.idacademiaarea = nactual.idacademiaarea
                    WHERE idestudiante = (
                        SELECT idestudiante 
                        FROM tbl_aca_estudiante 
                        WHERE idtestudiante = '" + this.IdTEstudiante + @"'
                    )
                    AND per.idperiodoacademico = (
                        SELECT idperiodoacademico 
                        FROM tbl_aca_periodoacademico 
                        WHERE idtperiodoacademico = " + this.Ejercicio + @"
                    )
                ) nactual ON nactual.idestudiante = est.idestudiante 

                LEFT JOIN (
                    SELECT 
                        matri.idestudiante, 
                        nactual.texto AS grado, 
                        areaactual.texto AS nivel
                    FROM tbl_aca_matricula matri
                    INNER JOIN tbl_aca_periodoacademico per 
                        ON per.idperiodoacademico = matri.idperiodoacademico
                    INNER JOIN tbl_aca_academianivel nactual 
                        ON nactual.idacademianivel = matri.idacademianivel 
                    INNER JOIN tbl_aca_academiaarea areaactual 
                        ON areaactual.idacademiaarea = nactual.idacademiaarea
                    WHERE idestudiante = (
                        SELECT idestudiante 
                        FROM tbl_aca_estudiante 
                        WHERE idtestudiante = '" + this.IdTEstudiante + @"'
                    )
                    AND per.idperiodoacademico = (
                        SELECT idperiodoacademico 
                        FROM tbl_aca_periodoacademico 
                        WHERE idtperiodoacademico = " + (this.Ejercicio + 1) + @"
                    )
                ) nsiguiente ON nsiguiente.idestudiante = est.idestudiante 

                INNER JOIN tbl_gen_moneda tgm 
                    ON tgm.idmoneda = cargo.idmoneda

                WHERE est.idtestudiante = '" + this.IdTEstudiante + @"'
                    AND (cargo.ejercicio = " + this.Ejercicio + @" OR cargo.ejercicio = " + (this.Ejercicio + 1) + @")
                    AND idservicio IN (7,2,39)
                    AND cargo.periodo = 1
                ORDER BY cargo.fechagrabacion DESC
            ";
        }

        public List<Viewestudiantesboletas> GetBoletas()
        {
            return AdapterUtil.ConvertDataTable<Viewestudiantesboletas>(this.MDataMapper?.GDatos.TraerDatosSQL(GetBoletasQuery()), this);
        }
    }
}