using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text;

namespace CAPA_NEGOCIO.Util
{
    public class StringUtil
    {

        public static string GeneratePassword(string email, string nombre, string apellido)
        {
            string password = string.Empty;
            Random random = new Random();
            // Verifica si el email es válido (contiene un @ y tiene texto antes y después del @)
            if (!string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.IndexOf("@") > 0 && email.IndexOf("@") < email.Length - 1)
            {
                string emailPrefix = email.Split('@')[0];
                // Si el prefijo tiene más de 3 caracteres, se recorta a los primeros 3
                if (emailPrefix.Length > 3)
                {
                    emailPrefix = emailPrefix.Substring(0, 3);
                }

                // Genera 3 números aleatorios
                int randomNumber = random.Next(100, 1000); // 3 dígitos
                password = $"{emailPrefix}{randomNumber}";
            }
            else
            {
                // Usa la primera letra del nombre y el primer apellido si el email no es válido
                string firstNameInitial = !string.IsNullOrWhiteSpace(nombre) ? nombre.Substring(0, 1).ToUpper() : "X";
                string lastName = !string.IsNullOrWhiteSpace(apellido) ? apellido.Split(' ')[0].ToUpper() : "Y";

                // Genera 2 números aleatorios
                int randomNumber = random.Next(10, 100); // 2 dígitos
                                                         // Combina la primera letra del nombre con el primer apellido y los números aleatorios
                password = $"{firstNameInitial}{lastName}{randomNumber}";
            }
            return password;
        }

        public static List<string> GetNombres(string cadena)
        {
            var nombres = cadena.Split(' ').ToList();
            if (nombres.Count < 2)
            {
                nombres.Add(null);
            }

            return nombres;
        }

    }
}