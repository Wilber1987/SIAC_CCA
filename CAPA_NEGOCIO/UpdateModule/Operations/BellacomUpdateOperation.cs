using APPCORE;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.Util;
using DataBaseModel;
using Microsoft.Extensions.Configuration;
using APPCORE.BDCore.Abstracts;

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

        public void updateBellacomDataOld()//TODO BORRAR CUANDO SE COMPRUEBE EL NUEVO
        {
            using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
            {
                siacSshClient.Connect();
                var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                siacTunnel.Start();
                try
                {
                    //manejo conexion por aparte, todo cambiar estas credenciales a configuracion
                    //var conection = SqlADOConexion.BuildDataMapper("DESKTOP-GJQ59U2\\SQLEXPRESS", "sa", "123", "SIAC_CCA_BEFORE_DEMO");  
                    var conection = SqlADOConexion.BuildDataMapper("BDSRV\\SQLCCA", "sa", "**$NIcca24@$PX", "SIAC_CCA_BEFORE_DEMO"); //TODO USAR CONFIGURACION
                    var tutor = new Parientes_Data_Update();
                    tutor.SetConnection(conection);
                    var filter = FilterData.And(
                                    FilterData.Or(
                                        FilterData.Distinc("migrado", true),
                                        FilterData.Equal("migrado", false),
                                        FilterData.ISNull("migrado")
                                    ),
                                    FilterData.Equal("Periodo_Lectivo_Update", 2025)
                                );

                    var tutores = tutor.Where<Parientes_Data_Update>(filter);
                    Console.WriteLine($"Número total de tutores encontrados: {tutores.Count}");
                    int tutorIndex = 0;

                    foreach (var t in tutores)
                    {
                        Console.WriteLine($"Procesando tutor {tutorIndex + 1} de {tutores.Count}, ID: {t.Id}");
                        tutorIndex++;

                        //obtenemos los datos de las nuevas actualizaciones para trasladarlos a bellacom
                        var updatedDataQuery = new UpdatedData
                        {
                            filterData = [
                                FilterData.JsonPropEqual("DataContract", "Id_Tutor_responsable", t.Id),
                                FilterData.JsonPropEqual("DataContract", "Year", DateTime.Now.Year.ToString())
                            ]
                        };

                        // updatedDataQuery.SetConnection(conection);
                        var updatedData = updatedDataQuery.Get<UpdatedData>();
                        //si se encutran datos en updatedData continuamos con la actualización
                        var ultimoRegistro = updatedData.OrderByDescending(e => e.Id).FirstOrDefault();

                        if (ultimoRegistro == null)
                        {
                            LoggerServices.AddMessageError("Error al actualizar datos a bellacom padre id: " + t.Id, new Exception("El padre con id: " + t.Id + " esta marcado como actualizado pero no se encontro datos en updatedData"));
                            continue;
                        }

                        var listaPadres = ultimoRegistro?.DataContract?.Tutores;
                        var misEstudiantes = ultimoRegistro?.DataContract?.Estudiantes;

                        Console.WriteLine($"Número de padres encontrados: {listaPadres?.Count ?? 0}");
                        Console.WriteLine($"Número de estudiantes encontrados: {misEstudiantes?.Count ?? 0}");


                        var datosOriginalesPadre = new List<Tbl_aca_tutor>();
                        var datosOriginalesEstudiantes = new List<Tbl_aca_estudiante>();
                        //actualizacion datos del padre
                        try
                        {
                            int padreIndex = 0;
                            foreach (var padre in listaPadres)
                            {
                                Console.WriteLine($"Actualizando padre en bellacom {padreIndex + 1} de {listaPadres.Count}, ID: {padre}");
                                padreIndex++;
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
                                        LoggerServices.AddMessageError("ERROR: updateBellacomData error actualizando padres, no se encontro el tutor en SIGE id:" + padre + ". el id del responsable de familia es : " + t.Id, new Exception("ERROR: updateBellacomData error actualizando padres, no se encontro el tutor en bellacom id:" + padre + ". el id del responsable de familia es : " + t.Id));
                                        continue;
                                    }

                                    //dataMsql.Fechanacimiento = datosaActualizacionPadre.Fecha_Nacimiento;
                                    dataMsql.Idestadocivil = datosaActualizacionPadre.Id_Estado_Civil;
                                    dataMsql.Idpais = (short?)datosaActualizacionPadre.Id_Pais;
                                    dataMsql.Idregion = (short?)datosaActualizacionPadre.Id_Region;
                                    dataMsql.Direccion = datosaActualizacionPadre.Direccion;
                                    dataMsql.Telefono = datosaActualizacionPadre.Telefono;
                                    dataMsql.Lugartrabajo = string.IsNullOrEmpty(datosaActualizacionPadre.Lugar_trabajo) ? datosaActualizacionPadre.Lugar_trabajo : dataMsql.Lugartrabajo;
                                    dataMsql.Telefonotrabajo = datosaActualizacionPadre.Telefono_trabajo == null ? "" : datosaActualizacionPadre.Telefono_trabajo;
                                    dataMsql.Email = datosaActualizacionPadre.Email;
                                    dataMsql.Idreligion = datosaActualizacionPadre.Id_religion;
                                    dataMsql.Exalumno = datosaActualizacionPadre.Ex_Alumno;
                                    dataMsql.Fechamodificacion = DateTime.Now.Date;
                                    dataMsql.Ejercicio = datosaActualizacionPadre.EgresoExAlumno;
                                    dataMsql.Celular = datosaActualizacionPadre.Celular;
                                    dataMsql.Noidentificacion = datosaActualizacionPadre.Identificacion;
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
                        }

                        foreach (var est in misEstudiantes)
                        {
                            var estudiante = new Estudiantes_Data_Update();
                            //estudiante.SetConnection(conection);

                            var estudiantesDataSql = estudiante.Where<Estudiantes_Data_Update>(FilterData.Equal("id", est)).FirstOrDefault();
                            if (estudiantesDataSql != null)
                            {


                                try
                                {
                                    var data = new Tbl_aca_estudiante();
                                    data.SetConnection(MySqlConnections.BellacomTest);
                                    var filterEstudiante = FilterData.And(
                                                    FilterData.Or(
                                                        FilterData.Distinc("migrado", true),
                                                        FilterData.Equal("migrado", false),
                                                        FilterData.ISNull("migrado")
                                                    ),
                                                    FilterData.Equal("idtestudiante", estudiantesDataSql.Codigo),
                                                    FilterData.Equal("Periodo_Lectivo_Update", 2025)
                                                );

                                    var dataMsql = data.Where<Tbl_aca_estudiante>(filterEstudiante).FirstOrDefault();
                                    dataMsql?.SetConnection(MySqlConnections.BellacomTest);

                                    if (dataMsql != null)
                                    {
                                        dataMsql.BeginGlobalTransaction();
                                        datosOriginalesEstudiantes.Add(dataMsql);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE

                                        dataMsql.Idreligion = estudiantesDataSql.Id_religion;
                                        dataMsql.Idpais = (short?)estudiantesDataSql.Id_pais;
                                        dataMsql.Idregion = (short?)estudiantesDataSql.Id_region;
                                        dataMsql.Direccion = estudiantesDataSql.Direccion;
                                        dataMsql.Vivecon = estudiantesDataSql.Vivecon;
                                        dataMsql.Colegio = estudiantesDataSql.Colegio_procede;
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

                                }

                            }
                        }

                        ultimoRegistro.Data_Before_Update_Alumnos = datosOriginalesEstudiantes;
                        ultimoRegistro.Data_Before_Update_Padres = datosOriginalesPadre;
                        ultimoRegistro.Update();

                        t.Migrado = true;
                        t.Update();
                    }
                    ;

                }
                catch (Exception ex)
                {
                    LoggerServices.AddMessageError("Error al actualizar datos a bellacom:", ex);
                    throw;
                }
                finally
                {
                    siacTunnel.Stop();
                    siacSshClient.Disconnect();
                }
            }
        }

        public void updateBellacomData()
        {
            using (var siacSshClient = _sshTunnelService.GetSshClient("Bellacom"))
            {
                siacSshClient.Connect();
                var siacTunnel = _sshTunnelService.GetForwardedPort("Bellacom", siacSshClient, 3308);
                siacTunnel.Start();

                try
                {
                    // Conexión SQL Server (SIAC)
                    var connection = SqlADOConexion.BuildDataMapper("BDSRV\\SQLCCA", "sa", "**$NIcca24@$PX", "SIAC_CCA_BEFORE_DEMO");
                    Console.WriteLine("Conexión a SQL Server establecida correctamente.");
                    MigratePadres(connection);
                    MigrateEstudiantes(connection);
                }
                catch (Exception ex)
                {
                    LoggerServices.AddMessageError("Error general en updateBellacomData:", ex);
                    throw;
                }
                finally
                {
                    siacTunnel.Stop();
                    siacSshClient.Disconnect();
                }
            }
        }

        private void MigratePadres(WDataMapper? connection)
        {
            var tutorRepo = new Parientes_Data_Update();
            var filter = FilterData.And(
                FilterData.Or(
                    FilterData.ISNull("migrado"),
                    FilterData.Equal("migrado", false)
                ),
                FilterData.Equal("Periodo_Lectivo_Update", 2025)//,
                ///FilterData.Equal("id_familia", 2727)
            );

            var tutores = tutorRepo.Where<Parientes_Data_Update>(filter);//2 rsultados
            Console.WriteLine($"Migrando {tutores.Count} padres...");
            var datosOriginalesPadre = new List<Tbl_aca_tutor>();
            int index = 0;
            foreach (var padre in tutores)
            {
                Console.WriteLine($"[{++index}/{tutores.Count}] Migrando padre ID: {padre.Id}");
                try
                {
                    var dataBellacom = new Tbl_aca_tutor();
                    dataBellacom.SetConnection(MySqlConnections.BellacomTest);

                    var registroBellacom = dataBellacom.Where<Tbl_aca_tutor>(FilterData.Equal("idtutor", padre.Id)).FirstOrDefault();

                    if (registroBellacom == null)
                    {
                        LoggerServices.AddMessageError($"No se encontró el tutor en Bellacom, ID: {padre.Id}", new Exception());
                        continue;
                    }

                    // Respaldar datos originales
                    //registroBellacom.BeginGlobalTransaction();
                    var dataMsql = dataBellacom.Where<Tbl_aca_tutor>(FilterData.Equal("idtutor", padre.Id)).FirstOrDefault();
                                dataMsql?.SetConnection(MySqlConnections.BellacomTest);                   

                    datosOriginalesPadre.Add(dataMsql);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE
                    // Actualizar datos
                    registroBellacom.Idestadocivil = padre.Id_Estado_Civil;
                    registroBellacom.Idpais = (short?)padre.Id_Pais;
                    registroBellacom.Idregion = (short?)padre.Id_Region;
                    registroBellacom.Direccion = padre.Direccion;
                    registroBellacom.Telefono = padre.Telefono;
                    registroBellacom.Lugartrabajo = padre.Lugar_trabajo;
                    registroBellacom.Telefonotrabajo = padre.Telefono_trabajo ?? "";
                    registroBellacom.Email = padre.Email;
                    registroBellacom.Idreligion = padre.Id_religion;
                    registroBellacom.Exalumno = padre.Ex_Alumno;
                    registroBellacom.Ejercicio = padre.EgresoExAlumno;
                    registroBellacom.Celular = padre.Celular;
                    registroBellacom.Noidentificacion = padre.Identificacion;
                    registroBellacom.Fechamodificacion = DateTime.Now.Date;

                    var respuesta = registroBellacom.Update();

                    if (respuesta.status != 200)
                    {
                        continue;
                    }
                    // Marcar como migrado
                    padre.Migrado = true;
                    padre.Update();
                }
                catch (Exception ex)
                {
                    LoggerServices.AddMessageError($"Error migrando padre ID: {padre.Id}", ex);
                }
            }
        }

        private void MigrateEstudiantes(WDataMapper? connection)
        {
            var estudianteRepo = new Estudiantes_Data_Update();
            //estudianteRepo.SetConnection(connection);
            var datosOriginalesEstudiantes = new List<Tbl_aca_estudiante>();
            
            var filter = FilterData.And(
                FilterData.Or(
                    FilterData.ISNull("migrado"),
                    FilterData.Equal("migrado", false)
                ),
                FilterData.Equal("Periodo_Lectivo_Update", 2025)//,
                ///FilterData.Equal("id_familia", 2727)
            );

            var estudiantes = estudianteRepo.Where<Estudiantes_Data_Update>(filter);
            Console.WriteLine($"Migrando {estudiantes.Count} estudiantes...");

            int index = 0;
            foreach (var est in estudiantes)
            {
                Console.WriteLine($"[{++index}/{estudiantes.Count}] Migrando estudiante ID: {est.Id}");
                try
                {
                    var dataBellacom = new Tbl_aca_estudiante();
                    dataBellacom.SetConnection(MySqlConnections.BellacomTest);
                   
                    var registroBellacom = dataBellacom.Where<Tbl_aca_estudiante>(
                        FilterData.Equal("idtestudiante", est.Codigo)                        
                    ).FirstOrDefault();
                    registroBellacom?.SetConnection(MySqlConnections.BellacomTest);
                                    
                    if (registroBellacom == null)
                    {
                        LoggerServices.AddMessageError($"No se encontró estudiante en Bellacom, ID: {est.Id}", new Exception());
                        continue;
                    }

                    //registroBellacom.BeginGlobalTransaction();
                    //datosOriginalesEstudiantes.Add(registroBellacom);//respaldo los datos para guardarlos en nuestra tabla de sqlServer, estos son los datos originales de SIGE

                    // Actualizar campos
                    registroBellacom.Idreligion = est.Id_religion;
                    registroBellacom.Idpais = (short?)est.Id_pais;
                    registroBellacom.Idregion = (short?)est.Id_region;
                    registroBellacom.Direccion = est.Direccion;
                    registroBellacom.Vivecon = est.Vivecon;
                    registroBellacom.Colegio = est.Colegio_procede;
                    registroBellacom.Sacramento = est.Sacramento;
                    registroBellacom.Aniosacra = est.Aniosacra;
                    registroBellacom.Fechamodificacion = DateTime.Now.Date;

                    var respuesta = registroBellacom.Update();

                    if (respuesta.status != 200)
                    {
                        continue;
                    }
                    est.Migrado = true;
                    est.Update();
                }
                catch (Exception ex)
                {
                    LoggerServices.AddMessageError($"Error migrando estudiante ID: {est.Id}", ex);
                }
            }
        }
    }
}