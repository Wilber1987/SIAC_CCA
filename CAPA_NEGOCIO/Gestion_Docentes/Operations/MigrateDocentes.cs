using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using Microsoft.Extensions.Configuration;


namespace CAPA_NEGOCIO.Oparations
{
    public class MigrateDocentes: TransactionalClass
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
            Console.Write("--> migrateDocentes");

            // Establecer conexión SSH y puerto de MySQL
            using (var siacSshClient = _sshTunnelService.GetSshClient("Siac"))
            {
                //siacSshClient.KeepAliveInterval = TimeSpan.FromSeconds(30);
                siacSshClient.Connect(); // Conectar al cliente SSH
                
                var siacTunnel = _sshTunnelService.GetForwardedPort("Siac", siacSshClient, 3307); 
                siacTunnel.Start(); // Iniciar el túnel

                // Iniciar la migración de docentes
                var docentes = new Docentes();
                docentes.SetConnection(MySqlConnections.SiacTest); // Usa la conexión MySQL a través del túnel SSH
                var docentesMsql = docentes.Get<Docentes>();

                try
                {
                    BeginGlobalTransaction(); // Inicia la transacción global

                    docentesMsql.ForEach(tn =>
                    {
                        var existingDocente = new Docentes()
                        {
                            Id = tn.Id
                        }.Find<Docentes>();

                        if (existingDocente != null && existingDocente.Updated_at != tn.Updated_at)
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
                            existingDocente.Update(); // Actualizar el docente existente
                        }
                        else if (existingDocente == null)
                        {
                            tn.Save(); // Guardar el nuevo docente
                        }
                    });

                    CommitGlobalTransaction(); // Confirmar la transacción global
                }
                catch (System.Exception ex)
                {
                    LoggerServices.AddMessageError("ERROR: migrateDocentes.Migrate.", ex);
                    // RollBackGlobalTransaction(); // Rollback en caso de error
                    // throw; // Lanza la excepción después de registrar el error
                }
                finally
                {
                    siacTunnel.Stop(); // Detener el túnel SSH
                    siacSshClient.Disconnect(); // Desconectar el cliente SSH
                }
            }

            return true;
        }
	}
}
