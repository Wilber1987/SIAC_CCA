using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;
using APPCORE;
using CAPA_NEGOCIO.Util;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;


namespace CAPA_NEGOCIO.Oparations
{
    public class MigrateDocentes : TransactionalClass
    {
        private readonly SshTunnelService _sshTunnelService;

        public MigrateDocentes()
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

        public async Task Migrate()
        {
            await migrateDocentes();
        }


        public async Task<bool> migrateDocentes()
        {
            LoggerServices.AddMessageInfo("migrateDocentes--> Iniciando migrateDocentes");
            ForwardedPortLocal siacTunnel = null;

            using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
            {
                try
                {
                    siacSshClient.Connect();
                    LoggerServices.AddMessageInfo("migrateDocentes--> Cliente SSH conectado correctamente.");

                    siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307);
                    siacTunnel.Start();
                    LoggerServices.AddMessageInfo("migrateDocentes--> Túnel SSH iniciado correctamente.");

                    var docentes = new Docentes();
                    docentes.SetConnection(MySqlConnections.SiacTest);
                    var docentesMsql = docentes.Get<Docentes>();

                    LoggerServices.AddMessageInfo($"migrateDocentes--> Docentes encontrados en MySQL: {docentesMsql.Count}");

                    //BeginGlobalTransaction();
                    int actualizados = 0;
                    int insertados = 0;

                    docentesMsql.ForEach(tn =>
                    {
                        var existingDocente = new Docentes()
                        {
                            Id = tn.Id
                        }.SimpleFind<Docentes>();

                        if (existingDocente != null)
                        {
                            existingDocente.Primer_nombre = tn.Primer_nombre;
                            existingDocente.Segundo_nombre = tn.Segundo_nombre;
                            existingDocente.Primer_apellido = tn.Primer_apellido;
                            existingDocente.Segundo_apellido = tn.Segundo_apellido;
                            existingDocente.Sexo = tn.Sexo;
                            existingDocente.Fecha_nacimiento = tn.Fecha_nacimiento;
                            existingDocente.Lugar_nacimiento = tn.Lugar_nacimiento;
                            existingDocente.Direccion = tn.Direccion;
                            existingDocente.Telefono = tn.Telefono;
                            existingDocente.Celular = tn.Celular;
                            existingDocente.Email = tn.Email;
                            existingDocente.Estado_civil_id = tn.Estado_civil_id;
                            existingDocente.Id_religion = tn.Id_religion;
                            existingDocente.Escolaridad_id = tn.Escolaridad_id;
                            existingDocente.Foto = tn.Foto;
                            existingDocente.Updated_at = tn.Updated_at;
                            existingDocente.Habilitado = tn.Habilitado;
                            existingDocente.Cargo = tn.Cargo;
                            existingDocente.Update();

                            actualizados++;
                            LoggerServices.AddMessageInfo($"migrateDocentes--> Docente actualizado: ID = {existingDocente.Id}");
                        }
                        else if (existingDocente == null)
                        {
                            tn.Save();
                            insertados++;
                            LoggerServices.AddMessageInfo($"migrateDocentes--> Nuevo docente insertado: ID = {tn.Id}");
                        }
                    });

                    //CommitGlobalTransaction();
                    LoggerServices.AddMessageInfo($"migrateDocentes--> Transacción completada. Docentes actualizados: {actualizados}, insertados: {insertados}");
                }
                catch (Exception ex)
                {
                    LoggerServices.AddMessageError("migrateDocentes--> ERROR: migrateDocentes", ex);
                    //RollBackGlobalTransaction(); // restaurar si falla
                }
                finally
                {
                    try
                    {
                        if (siacSshClient.IsConnected)
                        {
                            siacSshClient.Disconnect();
                            LoggerServices.AddMessageInfo("migrateDocentes--> Cliente SSH desconectado.");
                        }

                        LoggerServices.AddMessageInfo("migrateDocentes--> Deteniendo el túnel SSH.");
                        siacTunnel?.Stop();
                    }
                    catch (Exception ex)
                    {
                        LoggerServices.AddMessageError("migrateDocentes--> ERROR al cerrar conexión SSH o túnel.", ex);
                    }
                }
            }
            return true;
        }
    }
}
