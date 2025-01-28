using CAPA_DATOS;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CAPA_NEGOCIO.UpdateModule.Operations
{
    public class BellacomUpdateOperation : TransactionalClass
    {

        private readonly SshTunnelService _sshTunnelService;

        public BellacomUpdateOperation()
        {
            _sshTunnelService = new SshTunnelService(LoadConfiguration());
        }


        private IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public void updateBellacomData()
        {
            try
            {
                //manejo conexion por aparte, todo cambiar estas credenciales a configuracion
                //var conection = SqlADOConexion.BuildDataMapper("localhost\\SQLEXPRESS", "sa", "123", "SIAC_CCA_BEFORE_DEMO");                
                var conection = SqlADOConexion.BuildDataMapper("BDSRV\\SQLCCA", "sa", "**$NIcca24@$PX", "SIAC_CCA_BEFORE_DEMO");
                var tutor = new Parientes_Data_Update();
                tutor.SetConnection(conection);
                var filter = FilterData.Or(
                    FilterData.Distinc("migrado", true),
                    FilterData.Equal("migrado", false),
                    FilterData.ISNull("migrado")
                );
                tutor.filterData.Add(FilterData.NotNull("user_id"));                
                tutor.filterData.Add(FilterData.Equal("id_familia","1712"));                

                var tutores = tutor.Where<Parientes_Data_Update>(filter);

                foreach (var t in tutores)
                {
                    //obtenemos los datos de las nuevas actualizaciones para trasladarlos a bellacom
                    var updatedDataQuery = new UpdatedData
                    {
                        filterData = [new FilterData
                        {
                            ObjectName = "DataContract",
                            PropName = "Id_Tutor_responsable",
                            FilterType = "JSONPROP_EQUAL",
                            PropSQLType = "int",
                            Values = new List<string?> { t.Id.ToString() },
                        }]
                    };

                    updatedDataQuery.SetConnection(conection);
                    var updatedData = updatedDataQuery.Get<UpdatedData>();
                    //si se encutran datos en updatedData continuamos con la actualización
                    var ultimoRegistro = updatedData.OrderByDescending(e => e.Id).FirstOrDefault();

                    if (ultimoRegistro == null)
                    {
                        LoggerServices.AddMessageError("Error al actualizar datos a bellacom padre id: "+t.Id, new Exception("El padre con id: " + t.Id + " esta marcado como actualizado pero no se encontro datos en updatedData"));
                        continue;
                    }

                    var listaPadres = ultimoRegistro?.DataContract?.Tutores;
                    var misEstudiantes = ultimoRegistro?.DataContract?.Estudiantes;

                    var datosOriginalesPadre = new List<Tbl_aca_tutor>();
                    var datosOriginalesEstudiantes = new List<Tbl_aca_estudiante>();
         
                    
                    //actualizacion datos del padre
                    using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
                    {
                        siacSshClient.Connect();
                        var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                        siacTunnel.Start();
                        
                        try
                        {
                            foreach (var padre in listaPadres)
                            {
                                var data = new Tbl_aca_tutor();
                                data.SetConnection(MySqlConnections.BellacomTest);

                                var dataMsql = data.Where<Tbl_aca_tutor>(FilterData.Equal("idtutor", padre)).FirstOrDefault();
                                dataMsql?.SetConnection(MySqlConnections.BellacomTest);
                                if (dataMsql != null)
                                {
                                    dataMsql.BeginGlobalTransaction();
                                    datosOriginalesPadre.Add(dataMsql);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE
                                    var datosaActualizacionPadre = new Parientes_Data_Update().Where<Parientes_Data_Update>(FilterData.Equal("id", padre)).FirstOrDefault();
                                    if (datosaActualizacionPadre == null)
                                    {
                                        dataMsql.CommitGlobalTransaction();
                                        LoggerServices.AddMessageError("ERROR: updateBellacomData error actualizando padres, no se encontro el tutor en SIGE id:" + padre+ ". el id del responsable de familia es : "+t.Id, new Exception("ERROR: updateBellacomData error actualizando padres, no se encontro el tutor en bellacom id:" + padre+ ". el id del responsable de familia es : "+t.Id));
                                        continue;
                                    }
                                    
                                    //dataMsql.Fechanacimiento = datosaActualizacionPadre.Fecha_Nacimiento;
                                    dataMsql.Idestadocivil = datosaActualizacionPadre.Id_Estado_Civil;
                                    dataMsql.Idpais = (short?)datosaActualizacionPadre.Id_Pais;
                                    dataMsql.Idregion = (short?)datosaActualizacionPadre.Id_Region;
                                    dataMsql.Direccion = datosaActualizacionPadre.Direccion;
                                    dataMsql.Telefono = datosaActualizacionPadre.Telefono;
                                    dataMsql.Lugartrabajo = datosaActualizacionPadre.Lugar_trabajo;
                                    dataMsql.Telefonotrabajo = datosaActualizacionPadre.Telefono_trabajo == null ? "" : datosaActualizacionPadre.Telefono_trabajo;//**
                                    dataMsql.Email = datosaActualizacionPadre.Email;                                    
                                    dataMsql.Idreligion = datosaActualizacionPadre.Id_religion;
                                    dataMsql.Exalumno = datosaActualizacionPadre.Ex_Alumno;
                                    dataMsql.Fechamodificacion = DateTime.Now.Date;
                                    dataMsql.Ejercicio = datosaActualizacionPadre.EgresoExAlumno;
                                    dataMsql.Celular = datosaActualizacionPadre.Celular;//**
                                    dataMsql.Update();
                                    dataMsql.CommitGlobalTransaction();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write($"ERROR: updateBellacomData error actualizando tutores id: {t.Id}");
                            LoggerServices.AddMessageError("ERROR: updateBellacomData error actualizando tutores id:" + t.Id, ex);
                        }
                        finally
                        {
                            siacTunnel.Stop();
                            siacSshClient.Disconnect();
                        }
                    }

                    foreach (var est in misEstudiantes)
                    {
                        var estudiante = new Estudiantes_Data_Update();
                        estudiante.SetConnection(conection);

                        var estudiantesDataSql = estudiante.Where<Estudiantes_Data_Update>(FilterData.Equal("id", est)).FirstOrDefault();
                        if (estudiantesDataSql != null)
                        {                            
                            
                            using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
                            {
                                siacSshClient.Connect();
                                var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                                siacTunnel.Start();

                                try
                                {
                                    var data = new Tbl_aca_estudiante();
                                    data.SetConnection(MySqlConnections.BellacomTest);
                                    var dataMsql = data.Where<Tbl_aca_estudiante>(FilterData.Equal("idtestudiante", estudiantesDataSql.Codigo)).FirstOrDefault();
                                    dataMsql?.SetConnection(MySqlConnections.BellacomTest);

                                    if (dataMsql != null)
                                    {
                                        dataMsql.BeginGlobalTransaction();
                                        datosOriginalesEstudiantes.Add(dataMsql);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE
                    
                                        dataMsql.Idreligion = estudiantesDataSql.Id_religion;                                        
                                        dataMsql.Idpais =  (short?)estudiantesDataSql.Id_pais;
                                        dataMsql.Idregion = (short?)estudiantesDataSql.Id_region;
                                        dataMsql.Direccion = estudiantesDataSql.Direccion;
                                        dataMsql.Vivecon = estudiantesDataSql.Vivecon;
                                        dataMsql.Colegio =  estudiantesDataSql.Colegio_procede;
                                        dataMsql.Sacramento = estudiantesDataSql.Sacramento;
                                        dataMsql.Aniosacra = estudiantesDataSql.Aniosacra;
                                        dataMsql.Fechamodificacion = DateTime.Now.Date;

                                        dataMsql.Update();    
                                        dataMsql.CommitGlobalTransaction();                                   

                                    }
                                    else
                                    {
                                        Console.Write($"ERROR: updateBellacomData error actualizando estudiantes, no se encontro estudiante en bellacom id: {est}");
                                        LoggerServices.AddMessageError("ERROR: updateBellacomData error actualizando estudiantes, no se encontro estudiante en bellacom id:" + est, new Exception($"ERROR: updateBellacomData error actualizando estudiantes, no se encontro estudiante en bellacom id: {est}"));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.Write($"ERROR: updateBellacomData error actualizando estudiantes id: {est}");
                                    LoggerServices.AddMessageError("ERROR: updateBellacomData error actualizando estudiantes id:" + est, ex);
                                }
                                finally
                                {
                                    siacTunnel.Stop();
                                    siacSshClient.Disconnect();
                                }
                            }
                        }
                    }

                    ultimoRegistro.Data_Before_Update_Alumnos = datosOriginalesEstudiantes;
                    ultimoRegistro.Data_Before_Update_Padres = datosOriginalesPadre;
                    ultimoRegistro.Update();                  
                    
                    t.Migrado = true;
                    t.Update();
                };

            }
            catch (Exception ex)
            {
                LoggerServices.AddMessageError("Error al actualizar datos a bellacom:", ex);
                throw;
            }
        }
    }
}