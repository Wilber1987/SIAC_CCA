using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.BDCore.Abstracts;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateEstudiantes : TransactionalClass
	{

		public bool Migrate()
		{
			return migrateEstudiantes();// && migrateParientes() && migrateResponsables();
		}
		public bool migrateEstudiantes()
		{
			Console.Write("-->migrateEstudiantes");
			var estudiante = new Tbl_aca_estudiante();
			estudiante.SetConnection(MySqlConnections.Bellacom);
			var EstudiantesMsql = estudiante.Get<Tbl_aca_estudiante>(
			);
			try
			{
				BeginGlobalTransaction();
				EstudiantesMsql.ForEach(est =>
				{
					var existingEstudiante = new Estudiantes()
					{
						Id = est.Idestudiante
					}.Find<Estudiantes>();

					est.Fechanacimiento = DateUtil.ValidSqlDateTime(est.Fechanacimiento.GetValueOrDefault());
					est.Fechamodificacion = DateUtil.ValidSqlDateTime(est.Fechamodificacion.GetValueOrDefault());
					est.Fechagrabacion = DateUtil.ValidSqlDateTime(est.Fechagrabacion.GetValueOrDefault());
					if (existingEstudiante != null && existingEstudiante.Updated_at != est.Fechamodificacion)
                    {
                        buildEstudiante(est, existingEstudiante);

                        existingEstudiante.Update();
                    }
                    else if (existingEstudiante == null)
					{
						Estudiantes newEstudiante = new Estudiantes();
						buildEstudiante(est,newEstudiante);
						newEstudiante.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateEstudiantes.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

        private static void buildEstudiante(Tbl_aca_estudiante est, Estudiantes? existingEstudiante)
        {
            var estudianteJoin = new Estudiantes();
            estudianteJoin.SetConnection(MySqlConnections.Bellacom);
            var otrosDatos = estudianteJoin.Where<Estudiantes>(FilterData.Equal("codigo", est.Idtestudiante)).FirstOrDefault();
            if (otrosDatos != null)
            {
                existingEstudiante.Lugar_nacimiento = otrosDatos.Lugar_nacimiento;
                existingEstudiante.Madre_id = otrosDatos.Madre_id;
                existingEstudiante.Padre_id = otrosDatos.Padre_id;
                existingEstudiante.Foto = otrosDatos.Foto;
                existingEstudiante.Peso = otrosDatos.Peso;
                existingEstudiante.Altura = otrosDatos.Altura;
                existingEstudiante.Tipo_sangre = otrosDatos.Tipo_sangre;
                existingEstudiante.Padecimientos = otrosDatos.Padecimientos;
                existingEstudiante.Alergias = otrosDatos.Alergias;
                existingEstudiante.Recorrido_id = otrosDatos.Recorrido_id;
				existingEstudiante.Idtestudiante = otrosDatos.Idtestudiante;
            }


			existingEstudiante.Id = est.Idestudiante;			
            existingEstudiante.Primer_nombre = StringUtil.GetNombres(est.Nombres)[0];
            existingEstudiante.Segundo_nombre = StringUtil.GetNombres(est.Nombres)[1];
            existingEstudiante.Primer_apellido = StringUtil.GetNombres(est.Apellidos)[0];
            existingEstudiante.Segundo_apellido = StringUtil.GetNombres(est.Apellidos)[1];
            existingEstudiante.Fecha_nacimiento = est.Fechanacimiento;
            existingEstudiante.Sexo = est.Sexo;
            existingEstudiante.Direccion = est.Direccion;
            existingEstudiante.Codigo = est.Idtestudiante;
            existingEstudiante.Created_at = est.Fechagrabacion;
            existingEstudiante.Updated_at = est.Fechamodificacion;
            existingEstudiante.Id_familia = est.Idfamilia;
            existingEstudiante.Periodo = est.Periodo;
            existingEstudiante.Fecha_ingreso = est.Fechaingreso;
            existingEstudiante.Id_pais = est.Idpais;
            existingEstudiante.Id_sociedad = est.Idsociedad;
            existingEstudiante.Id_region = est.Idregion;
            existingEstudiante.Solvencia = est.Solvencia;
            existingEstudiante.Saldomd = est.Saldomd;
            existingEstudiante.Estatus = est.Estatus;
            existingEstudiante.Retenido = est.Retenido;
            existingEstudiante.Referencia_estatus = est.Referenciaestatus;
            existingEstudiante.Usuario_grabacion = est.Usuariograbacion;
            existingEstudiante.Usuario_modificacion = est.Usuariomodificacion;
            existingEstudiante.Id_old = est.Id_old;
            existingEstudiante.Id_cliente = est.Idcliente;
            existingEstudiante.Codigomed = est.Codigomed;
            existingEstudiante.Ump = est.Ump;
            existingEstudiante.Uep = est.Uep;
            existingEstudiante.Colegio = est.Colegio;
            existingEstudiante.Vivecon = est.Vivecon;
            existingEstudiante.Sacramento = est.Sacramento;
            existingEstudiante.Aniosacra = est.Aniosacra;
            existingEstudiante.Fecha_aceptacion = est.Fechaaceptacion;
            existingEstudiante.Usuario_aceptacion = est.Usuarioaceptacion;
            existingEstudiante.Aceptacion = est.Aceptacion;
            existingEstudiante.Periodo_aceptacion = est.Periodoaceptacion;
            existingEstudiante.Fechaun = est.Fechaun;
            existingEstudiante.Motivo = est.Motivo;
            existingEstudiante.Comentario = est.Comentario;
            existingEstudiante.Fecharetencion = est.Fecharetencion;
            existingEstudiante.Saldoeamd = est.Saldoeamd;
        }

        public bool migrateParientes()
		{
			Console.Write("-->migrateParientes");
			var Pariente = new Parientes();
			Pariente.SetConnection(MySQLConnection.SQLM);
			var ParientesMsql = Pariente.Get<Parientes>();
			try
			{
				BeginGlobalTransaction();
				ParientesMsql.ForEach(est =>
				{
					var existingPariente = new Parientes()
					{
						Id = est.Id
					}.Find<Parientes>();

					// est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
					// est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());

					if (existingPariente != null /*&& existingPariente.Updated_at != est.Updated_at*/)
					{
						existingPariente.Primer_nombre = est.Primer_nombre;
						existingPariente.Segundo_nombre = est.Segundo_nombre;
						existingPariente.Primer_apellido = est.Primer_apellido;
						existingPariente.Segundo_apellido = est.Segundo_apellido;
						existingPariente.Sexo = est.Sexo;
						existingPariente.Profesion = est.Profesion;
						existingPariente.Direccion = est.Direccion;
						existingPariente.Lugar_trabajo = est.Lugar_trabajo;
						existingPariente.Telefono = est.Telefono;
						existingPariente.Celular = est.Celular;
						existingPariente.Email = est.Email;
						existingPariente.Estado_civil_id = est.Estado_civil_id;
						existingPariente.Religion_id = est.Religion_id;
						existingPariente.Update();
					}
					else if (existingPariente == null)
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateParientes.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}

		public bool migrateResponsables()
		{
			Console.Write("-->migrateResponsables");
			var responsable = new Responsables();
			responsable.SetConnection(MySQLConnection.SQLM);
			var responsablesMsql = responsable.Get<Responsables>();
			try
			{
				BeginGlobalTransaction();
				responsablesMsql.ForEach(est =>
				{
					var existingResponsable = new Responsables()
					{
						Id = est.Id
					}.Find<Responsables>();

					est.Updated_at = DateUtil.ValidSqlDateTime(est.Updated_at.GetValueOrDefault());
					est.Created_at = DateUtil.ValidSqlDateTime(est.Created_at.GetValueOrDefault());
					if (existingResponsable != null && existingResponsable.Updated_at != est.Updated_at)
					{
						existingResponsable.Estudiante_id = est.Estudiante_id;
						existingResponsable.Pariente_id = est.Pariente_id;
						existingResponsable.Updated_at = est.Updated_at;
						existingResponsable.Parentesco = est.Parentesco;
						//existingResponsable.Update();
					}
					else if (existingResponsable == null)
					{
						est.Save();
					}

				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: migrateResponsables.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
		}
	}
}
