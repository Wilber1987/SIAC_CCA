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
        public string? Grado_Actual { get; set; }
        public string? Curso_Actual { get; set; }
        public string? Grado_Siguiente { get; set; }
        public string? Curso_Siguiente { get; set; }
        public int? idfamilia { get; set; }


        public string GetBoletasQuery()
        {
            return @"SELECT  per.idtperiodoacademico, cargo.ejercicio, cargo.tipo, cargo.idperiodoacademico, cargo.idestudiante, est.idtestudiante, cargo.periodo, cargo.idmoneda, tgm.idtmoneda, sum(cargo.importemd) as importemd, sum(cargo.descuento) as descuento, sum(cargo.importedescuentomd) as importedescuentomd
                        , sum(cargo.importenetomd) as importenetomd, cargo.estatus, cargo.contabilizado, cargo.fechagrabacion, est.idestudiante as codigo, est.nombres, est.apellidos, YEAR(current_date)as ciclo
                        , areaactual.texto as grado_actual, nactual.texto as curso_actual, areasiguiente.texto as grado_siguiente, ifnull(nsiguiente.texto,'N/D') as curso_siguiente
                            from tbl_aca_estudiantecargo cargo
                                inner join tbl_aca_estudiante est on est.idestudiante = cargo.idestudiante
                                inner join tbl_aca_matricula matri on matri.idestudiante = est.idestudiante
                                inner join tbl_aca_periodoacademico per on per.idperiodoacademico = matri.idperiodoacademico
                                inner join tbl_aca_academianivel nactual on nactual.idacademianivel = matri.idacademianivel
                                left join tbl_aca_academianivel nsiguiente on nsiguiente.idacademianivel = nactual.idnivelproximo
                                INNER JOIN tbl_aca_academiaarea areaactual on areaactual.idacademiaarea = nactual.idacademiaarea
                                INNER JOIN tbl_aca_academiaarea areasiguiente on areasiguiente.idacademiaarea = nsiguiente.idacademiaarea
                                inner join tbl_gen_moneda tgm on tgm.idmoneda  = cargo.idmoneda
                            where 	est.idtestudiante  = '" + this.IdTEstudiante + @"'
                                and cargo.ejercicio = " + this.Ejercicio + @"
                                and per.idtperiodoacademico = " + (this.Ejercicio + 1) + @"
                                and idservicio in (7,2,39)
                                and cargo.contabilizado = 0
                            group by per.idtperiodoacademico, cargo.ejercicio,cargo.tipo, cargo.idperiodoacademico, cargo.idestudiante, est.idtestudiante, cargo.periodo, cargo.idmoneda,tgm.idtmoneda
                        , cargo.estatus, cargo.contabilizado, cargo.fechagrabacion, est.idestudiante, est.nombres, est.apellidos, YEAR(current_date)
                        ,areaactual.texto, nactual.texto, areasiguiente.texto, nsiguiente.texto
                            order by fechagrabacion desc";
        }
        public List<Viewestudiantesboletas> GetBoletas(){

            return AdapterUtil.ConvertDataTable<Viewestudiantesboletas>(this.MTConnection?.GDatos.TraerDatosSQL(GetBoletasQuery()), this);
        }
    }
}
