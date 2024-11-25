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
                var conection = SqlADOConexion.BuildDataMapper("localhost\\SQLEXPRESS", "sa", "123", "SIAC_CCA_BEFORE_DEMO");
                var tutor = new Parientes_Data_Update();
                tutor.SetConnection(conection);
                var filter = FilterData.Or(
                    FilterData.Distinc("migrado", true),
                    FilterData.Equal("migrado", false),
                    FilterData.ISNull("migrado")
                );
                tutor.filterData.Add(FilterData.Equal("id",5046));//todo quitar en produccion
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

                    var datosOriginalesPadre = new List<string>();
                    var datosOriginalesEstudiantes = new List<string>();
                    
                    //actualizacion datos del padre
                    /*using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
                    {
                        siacSshClient.Connect();
                        var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                        siacTunnel.Start();*/
                        
                        try
                        {
                            foreach (var padre in listaPadres)
                            {
                                var data = new Tbl_aca_tutor();
                                data.SetConnection(MySqlConnections.BellacomTest);

                                var dataMsql = data.Where<Tbl_aca_tutor>(FilterData.Equal("idtutor", padre)).FirstOrDefault();
                                if (dataMsql != null)
                                {
                                    string json = JsonConvert.SerializeObject(dataMsql);
                                    datosOriginalesPadre.Add(json);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE

                                    dataMsql.Nombres = t.Primer_nombre + " " + t.Segundo_nombre;
                                    dataMsql.Apellidos = t.Primer_apellido + " " + t.Segundo_apellido;
                                    dataMsql.Sexo = t.Sexo;
                                    dataMsql.Fechanacimiento = t.Fecha_Nacimiento;
                                    dataMsql.Idestadocivil = t.Id_Estado_Civil;
                                    dataMsql.Idpais = (short?)t.Id_Pais;
                                    dataMsql.Idregion = (short?)t.Id_Region;
                                    dataMsql.Direccion = t.Direccion;
                                    dataMsql.Telefono = t.Telefono;
                                    dataMsql.Lugartrabajo = t.Lugar_trabajo;
                                    dataMsql.Telefonotrabajo = t.Telefono_trabajo;
                                    dataMsql.Email = t.Email;
                                    //dataMsql.//religion
                                    dataMsql.Idreligion = t.Id_religion;
                                    dataMsql.Exalumno = t.Ex_Alumno;
                                    dataMsql.Fechamodificacion = DateTime.Now.Date;
                                    dataMsql.Update();

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
                            /*siacTunnel.Stop();
                            siacSshClient.Disconnect();*/
                        }
                    //}

                    foreach (var est in misEstudiantes)
                    {
                        var estudiante = new Estudiantes_Data_Update();
                        estudiante.SetConnection(conection);

                        var estudiantesDataSql = estudiante.Where<Estudiantes_Data_Update>(FilterData.Equal("id", est)).FirstOrDefault();
                        if (estudiantesDataSql != null)
                        {
                            
                            
                            /*using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
                            {
                                siacSshClient.Connect();
                                var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                                siacTunnel.Start();*/

                                try
                                {
                                    var data = new Tbl_aca_estudiante();
                                    data.SetConnection(MySqlConnections.BellacomTest);
                                    var dataMsql = data.Where<Tbl_aca_estudiante>(FilterData.Equal("idtestudiante", estudiantesDataSql.Codigo)).FirstOrDefault();

                                    if (dataMsql != null)
                                    {
                                        string json = JsonConvert.SerializeObject(dataMsql);
                                        datosOriginalesEstudiantes.Add(json);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE

                                        dataMsql.Nombres = estudiantesDataSql.Primer_nombre + " " + estudiantesDataSql.Segundo_nombre;
                                        dataMsql.Apellidos = estudiantesDataSql.Primer_apellido + " " + estudiantesDataSql.Segundo_apellido;
                                        dataMsql.Idreligion = estudiantesDataSql.Id_religion;
                                        dataMsql.Fechanacimiento = estudiantesDataSql.Fecha_nacimiento;
                                        dataMsql.Idpais = (short?)estudiantesDataSql.Id_pais;
                                        dataMsql.Idregion = (short?)estudiantesDataSql.Id_region;
                                        dataMsql.Direccion = estudiantesDataSql.Direccion;
                                        dataMsql.Vivecon = estudiantesDataSql.Vivecon;
                                        dataMsql.Colegio = estudiantesDataSql.Colegio_procede;
                                        dataMsql.Idreligion = estudiantesDataSql.Id_religion;
                                        dataMsql.Sacramento = estudiantesDataSql.Sacramento;
                                        dataMsql.Aniosacra = estudiantesDataSql.Aniosacra;
                                        dataMsql.Fechamodificacion = DateTime.Now.Date;

                                        dataMsql.Update();                                       

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
                                    /*siacTunnel.Stop();
                                    siacSshClient.Disconnect();*/
                                }
                            }
                        //}
                    }
                    ultimoRegistro.Data_Before_Update_Padres = JsonConvert.SerializeObject(datosOriginalesPadre);
                    ultimoRegistro.Data_Before_Update_Alumnos = JsonConvert.SerializeObject(datosOriginalesEstudiantes);
                    //ultimoRegistro.Update();
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