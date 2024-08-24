using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
namespace DataBaseModel
{
	public class Conversacion : EntityClass
	{
		[PrimaryKey(Identity = true)]
		public int? Id_conversacion { get; set; }
		public string? Descripcion { get; set; }
        public DateTime? Fecha_Ultimo_Mensaje { get; set; }

        [OneToMany(TableName = "Conversacion_usuarios", KeyColumn = "Id_conversacion", ForeignKeyColumn = "Id_conversacion")]
		public List<Conversacion_usuarios>? Conversacion_usuarios { get; set; }

		[OneToMany(TableName = "Mensajes", KeyColumn = "Id_conversacion", ForeignKeyColumn = "Id_conversacion")]
		public List<Mensajes>? Mensajes { get; set; }

		public static List<Conversacion> GetConversaciones(string? identity)
		{
			UserModel user = AuthNetCore.User(identity);
			List<Conversacion_usuarios> Conversaciones_usuarios = new Conversacion_usuarios
			{ Id_usuario = user.UserId }.Get<Conversacion_usuarios>();
			return [.. Conversaciones_usuarios.Select(u => {
                DateTime? last = u.Conversacion?.Mensajes?.Select(m => m.Created_at).ToList().Max();
                if (u.Conversacion != null)
                {
                    u.Conversacion.Fecha_Ultimo_Mensaje = last;
                }                
                return u.Conversacion;                
            }).OrderByDescending(c => c?.Fecha_Ultimo_Mensaje)];
        }

        public Conversacion? FindConversacion(string? identity)
        {
            UserModel user = AuthNetCore.User(identity);
            List<Conversacion_usuarios> conversaciones_usuarios = new Conversacion_usuarios
			{ Id_usuario = user.UserId }.Get<Conversacion_usuarios>();

            List<Conversacion> conversaciones = new Conversacion{}.Where<Conversacion>(
                FilterData.In("id_conversacion", conversaciones_usuarios.Select(cu => cu.Id_conversacion).ToArray())
            );
            Conversacion? conversacion = conversaciones.Find(c => c.Conversacion_usuarios?.Find(cu => cu.Id_usuario == user.UserId) != null );
            if (conversacion != null)
            { 
                return conversacion;                
            }
           
            var profile = new Security_Users {Id_User = Conversacion_usuarios?[0].Id_usuario}.Get_Profile();
            Descripcion = profile?.GetNombreCompleto();
            Conversacion_usuarios?.Add(new Conversacion_usuarios{
                Id_usuario = user.UserId
            });
            return (Conversacion?)Save() ?? this;
        }

        public object? SaveConversacion(string? identity)
        {
            UserModel user = AuthNetCore.User(identity);
            if (Conversacion_usuarios == null || Conversacion_usuarios.Count == 0)
            {
                throw new Exception("no se puede crear una conversacion sin participantes");
            }
            //EN ESTE CASO SE REALIZA DE ESTA FORMA PARA MANEJAR UNA CONVERSACION POR PAREJA DE USUARIOS
            var conversacionesExistentes = GetConversaciones(identity);
            var conversacion = conversacionesExistentes
                .Where(c => UsuarioIncluidoEnConversacion(c?.Conversacion_usuarios))
                .FirstOrDefault();
            if (conversacion != null) { 
                return conversacion;
            }
            Conversacion_usuarios.Add(new Conversacion_usuarios { Id_usuario = user.UserId });
            return Save();
        }

        private bool UsuarioIncluidoEnConversacion(List<Conversacion_usuarios>? Conversacion_usuarios)
        {
            return Conversacion_usuarios?.Find(cu => cu.Id_usuario == Conversacion_usuarios.First().Id_usuario) != null;
        }
    }
}
