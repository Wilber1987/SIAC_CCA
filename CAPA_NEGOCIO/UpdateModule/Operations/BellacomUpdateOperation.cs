using CAPA_DATOS;
using CAPA_NEGOCIO.UpdateModule.Model;
using DataBaseModel;
using Newtonsoft.Json;

namespace CAPA_NEGOCIO.UpdateModule.Operations
{
    public class BellacomUpdateOperation : TransactionalClass
    {

        public void updateBellacomData()
        {
            try
            {
                //manejo conexion por aparte, todo cambiar estas credenciales a configuracion
                var conection = SqlADOConexion.BuildDataMapper("localhost\\SQLEXPRESS", "sa", "123", "SIAC_CCA_BEFORE_DEMO");
                var tutor = new Parientes_Data_Update();
                tutor.SetConnection(conection);
                var filter = FilterData.Or(
                    FilterData.Distinc("migrado", true),
                    FilterData.Equal("migrado", false),
                    FilterData.ISNull("migrado")
                );

                var tutores = tutor.Where<Parientes_Data_Update>(filter);

                tutores.ForEach(t =>
                {
                    //obtenemos los datos de las nuevas actualizaciones para trasladarlos a bellacom
                    var datosActualizacion = new UpdatedData();
                    //datosActualizacion.SetConnection(conection);

                    var mela = datosActualizacion.Get<UpdatedData>;

                    
                    var updateDate = new UpdatedData
                    {
                        filterData = [new FilterData
                        {
                            ObjectName = "DataContract",
                            PropName = "Id_Tutor_responsable",
                            FilterType = "JSONPROP_EQUAL",
                            PropSQLType = "int",
                            Values = new List<string?> { 4809.ToString() },
                        }]
                    };

                    var carjoa = updateDate.Get<UpdatedData>();     
                    var mela2 = carjoa;          
                });

            }
            catch (Exception ex)
            {
                LoggerServices.AddMessageError("Error al actualizar datos a bellacom:", ex);
                throw;
            }
        }
    }
}