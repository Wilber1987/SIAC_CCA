using CAPA_DATOS;
namespace DataBaseModel
{
    public class Regiones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id_region { get; set; }
        public int? Idtregion { get; set; }
        public string? Texto { get; set; }
        public int? Id_pais { get; set; }
    }
}