using APPCORE;
namespace DataBaseModel
{
    public class Regiones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id_region { get; set; }
        public string? Idtregion { get; set; }
        public string? Texto { get; set; }
        public int? Id_pais { get; set; }
    }
}