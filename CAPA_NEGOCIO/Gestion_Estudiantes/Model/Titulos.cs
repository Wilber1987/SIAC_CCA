using CAPA_DATOS;
namespace DataBaseModel
{
    public class Titulos : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id_Titulo { get; set; }        
        public string? Texto { get; set; }
    }
}