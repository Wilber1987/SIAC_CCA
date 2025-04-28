using BusinessLogic.Notificaciones_Mensajeria.Gestion_Notificaciones.Operations;
using CAPA_DATOS.Cron.Jobs;
using DataBaseModel;

namespace BackgroundJob.Cron.Jobs
{
    public class SendMailNotificationsSchedulerJob : CronBackgroundJob
    {
        private readonly ILogger<SendMailNotificationsSchedulerJob> _log;

        public SendMailNotificationsSchedulerJob(CronSettings<SendMailNotificationsSchedulerJob> settings, ILogger<SendMailNotificationsSchedulerJob> log)
            : base(settings.CronExpression, settings.TimeZone)
        {
            _log = log;
        }

        protected override async Task<Task> DoWork(CancellationToken stoppingToken)
        {
            _log.LogInformation(":::::::::::Running...  SendMailNotificationsSchedulerJob at {0}", DateTime.UtcNow);
            //CARGA AUTOMATICA DE CASOS
            try
            {
                //ENVIO DE NOTIFICACIONES
                NotificationSenderOperation.SendNotifications();
            }
            catch (Exception ex)
            {
                _log.LogInformation(":::::::::::ERROR... at {0}", ex);
            }

            return Task.CompletedTask;
        }
    }
    public class DailyCronJob : CronBackgroundJob
    {
        private readonly ILogger<DailyCronJob> _log;

        public DailyCronJob(CronSettings<DailyCronJob> settings, ILogger<DailyCronJob> log)
            : base(settings.CronExpression, settings.TimeZone)
        {
            _log = log;
        }

        protected override Task DoWork(CancellationToken stoppingToken)
        {
            _log.LogInformation(":::::::::::Running...  DailyCronJob at {0}", DateTime.UtcNow);
            try
            {

            }
            catch (System.Exception ex)
            {
                _log.LogInformation(":::::::::::ERROR  CALCULANDO MORA... at {0}", ex);
            }

            return Task.CompletedTask;
        }

        private IEnumerable<object> Get<T>()
        {
            throw new NotImplementedException();
        }
    }
}

