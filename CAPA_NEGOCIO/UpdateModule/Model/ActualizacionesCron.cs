using CAPA_DATOS;
using CAPA_DATOS.Services;
using DataBaseModel;

namespace CAPA_NEGOCIO.UpdateModule.Model
{
    public class ActualizacionesCron : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }

        public string? Descripcion { get; set; } 

        public DateTime? Fecha_Actualizacion { get; set; } 
    }
}