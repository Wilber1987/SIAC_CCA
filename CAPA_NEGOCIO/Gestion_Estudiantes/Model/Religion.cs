using CAPA_DATOS;
namespace DataBaseModel
{
    public class Religiones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public int? idtreligion { get; set; }
        public string? texto { get; set; }
    }
}