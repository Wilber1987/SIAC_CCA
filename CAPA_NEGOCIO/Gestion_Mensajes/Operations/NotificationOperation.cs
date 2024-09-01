using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_DATOS.Services;
using DataBaseModel;
using Npgsql.Replication.PgOutput.Messages;

namespace CAPA_NEGOCIO.Gestion_Mensajes.Operations
{
    public class NotificationOperation : TransactionalClass
    {
        
        public object? SaveNotificacion(string identity, NotificationRequest request)
        {
            try
            {
                foreach (var file in request.Files ?? [])
                {
                    ModelFiles? Response = (ModelFiles?)FileService.upload("Attach\\", file).body;
                    file.Value = Response?.Value;
                    file.Type = Response?.Type;
                }
                //logica
                //BeginGlobalTransaction();
                //Logical de guardado
                //try { new notificacionEntity { Files = request.Files ?? [] ...}.Save(); } ....


                //CommitGlobalTransaction();
                return this;
            }
            catch (System.Exception)
            {
                //RollBackGlobalTransaction();
                throw;
            }
        }
    }
}