using CAPA_DATOS.Cron.Jobs;
using CAPA_NEGOCIO.Oparations;
using CAPA_NEGOCIO.UpdateModule.Operations;
using DataBaseModel;

namespace BackgroundJob.Cron.Jobs
{
	public class MigrateEstudiantesCronJob : CronBackgroundJob
	{
		private readonly ILogger<MigrateEstudiantesCronJob> _log;

		public MigrateEstudiantesCronJob(CronSettings<MigrateEstudiantesCronJob> settings, ILogger<MigrateEstudiantesCronJob> log)
			: base(settings.CronExpression, settings.TimeZone)
		{
			_log = log;
		}

		protected override Task DoWork(CancellationToken stoppingToken)
		{
			_log.LogInformation(":::::::::::Running...  MigrateEstudiantesCronJob at {0}", DateTime.UtcNow);
			try
			{
				var job = new MigrateEstudiantes().Migrate();
			}
			catch (System.Exception ex)
			{
				_log.LogInformation(":::::::::::ERROR  MigrateEstudiantesCronJob... at {0}", ex);
			}

			return Task.CompletedTask;
		}

		private IEnumerable<object> Get<T>()
		{
			throw new NotImplementedException();
		}
	}

	public class MigrateDocentesCronJob : CronBackgroundJob
	{
		private readonly ILogger<MigrateDocentesCronJob> _log;

		public MigrateDocentesCronJob(CronSettings<MigrateDocentesCronJob> settings, ILogger<MigrateDocentesCronJob> log)
			: base(settings.CronExpression, settings.TimeZone)
		{
			_log = log;
		}

		protected override Task DoWork(CancellationToken stoppingToken)
		{
			_log.LogInformation(":::::::::::Running...  MigrateDocentesCronJob at {0}", DateTime.UtcNow);
			try
			{
				var job = new MigrateDocentes().Migrate();
			}
			catch (System.Exception ex)
			{
				_log.LogInformation(":::::::::::ERROR  MigrateDocentesCronJob... at {0}", ex);
			}

			return Task.CompletedTask;
		}

		private IEnumerable<object> Get<T>()
		{
			throw new NotImplementedException();
		}
	}

	public class MigrateGestionCursosCronJob : CronBackgroundJob
	{
		private readonly ILogger<MigrateGestionCursosCronJob> _log;

		public MigrateGestionCursosCronJob(CronSettings<MigrateGestionCursosCronJob> settings, ILogger<MigrateGestionCursosCronJob> log)
			: base(settings.CronExpression, settings.TimeZone)
		{
			_log = log;
		}

		protected override Task DoWork(CancellationToken stoppingToken)
		{
			_log.LogInformation(":::::::::::Running...  MigrateGestionCursosCronJob at {0}", DateTime.UtcNow);
			try
			{
				var job = new MigrateGestionCursos().Migrate();
			}
			catch (System.Exception ex)
			{
				_log.LogInformation(":::::::::::ERROR  MigrateGestionCursosCronJob... at {0}", ex);
			}

			return Task.CompletedTask;
		}

		private IEnumerable<object> Get<T>()
		{
			throw new NotImplementedException();
		}
	}

	public class MigrateNotasCronJob : CronBackgroundJob
	{
		private readonly ILogger<MigrateNotasCronJob> _log;

		public MigrateNotasCronJob(CronSettings<MigrateNotasCronJob> settings, ILogger<MigrateNotasCronJob> log)
			: base(settings.CronExpression, settings.TimeZone)
		{
			_log = log;
		}

		protected override Task DoWork(CancellationToken stoppingToken)
		{
			_log.LogInformation(":::::::::::Running...  MigrateNotasCronJob at {0}", DateTime.UtcNow);
			try
			{
				var job = new MigrateNotas().Migrate();
			}
			catch (System.Exception ex)
			{
				_log.LogInformation(":::::::::::ERROR  MigrateNotasCronJob... at {0}", ex);
			}

			return Task.CompletedTask;
		}

		private IEnumerable<object> Get<T>()
		{
			throw new NotImplementedException();
		}
	}

	public class SendInvitationToUpdateCronJob : CronBackgroundJob
	{
		private readonly ILogger<SendInvitationToUpdateCronJob> _log;

		public SendInvitationToUpdateCronJob(CronSettings<SendInvitationToUpdateCronJob> settings, ILogger<SendInvitationToUpdateCronJob> log)
			: base(settings.CronExpression, settings.TimeZone)
		{
			_log = log;
		}

		protected override Task DoWork(CancellationToken stoppingToken)
		{
			_log.LogInformation(":::::::::::Running...  SendInvitationToUpdateCronJob at {0}", DateTime.UtcNow);
			try
			{				
				new UpdateOperation().sendInvitations();
			}
			catch (System.Exception ex)
			{
				_log.LogInformation(":::::::::::ERROR  SendInvitationToUpdateCronJob... at {0}", ex);
			}

			return Task.CompletedTask;
		}

		private IEnumerable<object> Get<T>()
		{
			throw new NotImplementedException();
		}
	}
}