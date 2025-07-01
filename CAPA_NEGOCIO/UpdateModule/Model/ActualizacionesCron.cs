using APPCORE;
using APPCORE.Services;
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