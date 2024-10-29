using CAPA_DATOS;
namespace DataBaseModel
{
    public class Paises : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id_pais { get; set; }
        public int? Idtpais { get; set; }
        public string? Texto { get; set; }
    }
}