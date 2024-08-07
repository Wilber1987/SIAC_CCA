using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPA_DATOS;
using CAPA_NEGOCIO.Util;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
	public class MigrateNotas : TransactionalClass
	{
        public bool Migrate(){
            var tipoNotas = new Tipo_notas();
			tipoNotas.SetConnection(MySQLConnection.SQLM);
			var tipoNotasMsql = tipoNotas.Get<Tipo_notas>();
			try
			{
				BeginGlobalTransaction();
				tipoNotasMsql.ForEach(tn =>
				{
					tn.Created_at = DateUtil.ValidSqlDateTime(tn.Created_at.GetValueOrDefault());
                    tn.Updated_at = DateUtil.ValidSqlDateTime(tn.Updated_at.GetValueOrDefault());
					if (tn.Exists<Tipo_notas>())
					{
                        tn.Nombre = tn.Nombre;
                        tn.Nombre_corto = tn.Nombre_corto;
                        tn.Periodo_lectivo_id = tn.Periodo_lectivo_id;
                        tn.Consolidado_id = tn.Consolidado_id;
                        tn.Numero_consolidados = tn.Numero_consolidados;
                        tn.Update();
                        
					} else 
					{                        
						tn.Save();
					}					
					
				});
				CommitGlobalTransaction();
			}
			catch (System.Exception ex)
			{
				LoggerServices.AddMessageError("ERROR: MigrateNotas.Migrate.", ex);
				RollBackGlobalTransaction();
				throw;
			}

			return true;
        }
    }
}