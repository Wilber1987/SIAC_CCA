using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
    public class Tbl_aca_estudiante : EntityClass
    {
        [PrimaryKey(Identity = false)]
        public int? Idestudiante { get; set; }
        public string? Idtestudiante { get; set; }
        public int? Idsociedad { get; set; }
        public int? Periodo { get; set; }
        public DateTime? Fechaingreso { get; set; }
        public int? Idfamilia { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public DateTime? Fechanacimiento { get; set; }
        public int? Idreligion { get; set; }
        public string? Direccion { get; set; }
        public short? Idpais { get; set; }
        public short? Idregion { get; set; }
        public string? Email { get; set; }
        public string? Solvencia { get; set; }
        public Double? Saldomd { get; set; }
        public string? Estatus { get; set; }
        public bool? Retenido { get; set; }
        public string? Referenciaestatus { get; set; }
        public DateTime? Fechagrabacion { get; set; }
        public string? Usuariograbacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
        public string? Usuariomodificacion { get; set; }
        public string? Id_old { get; set; }
        public int? Idcliente { get; set; }
        public string? Codigomed { get; set; }
        public int? Ump { get; set; }
        public int? Uep { get; set; }
        public string? Colegio { get; set; }
        public string? Vivecon { get; set; }
        public string? Sacramento { get; set; }
        public int? Aniosacra { get; set; }
        public DateTime? Fechaaceptacion { get; set; }
        public string? Usuarioaceptacion { get; set; }
        public bool? Aceptacion { get; set; }
        public int? Periodoaceptacion { get; set; }
        public DateTime? Fechaun { get; set; }
        public string? Motivo { get; set; }
        public string? Comentario { get; set; }
        public DateTime? Fecharetencion { get; set; }
        public Double? Saldoeamd { get; set; }
        public string? Foto { get; set; }
    }

    public class ViewEstudiantesMigracion : Tbl_aca_estudiante
    {
        public int? Id { get; set; }

        public object? CreateView()
        {
            String query = "DROP VIEW IF EXISTS viewEstudiantesMigracion;CREATE VIEW viewEstudiantesMigracion AS SELECT e.id, e.foto, tae.* FROM tbl_aca_estudiante tae INNER JOIN estudiantes e ON e.codigo = tae.idtestudiante;";
            return ExecuteSqlQuery(query);
        }

        public object? DestroyView(String view)
        {
            string query = $"DROP VIEW IF EXISTS {view};";
            return ExecuteSqlQuery(query);
        }

        public object? CreateViewEstudiantesActivos()
        {
            int currentYear = MigrationDates.GetCurrentYear();

            string query = $"DROP VIEW IF EXISTS viewEstudiantesActivosSiac; " +
                           $"CREATE VIEW viewEstudiantesActivosSiac AS " +
                           $"SELECT e.* " +
                           $"FROM estudiantes e " +
                           $"INNER JOIN estudiante_clases ec ON ec.estudiante_id = e.id " +
                           $"INNER JOIN periodo_lectivos pl ON pl.id = ec.periodo_lectivo_id " +
                           $"WHERE pl.nombre_corto = '{currentYear}';";

            return ExecuteSqlQuery(query);
        }

    }
}
