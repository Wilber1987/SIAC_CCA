using CAPA_DATOS;
namespace DataBaseModel
{
    public class Profesiones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id_profesion { get; set; }        
        public string? Texto { get; set; }
    }
}