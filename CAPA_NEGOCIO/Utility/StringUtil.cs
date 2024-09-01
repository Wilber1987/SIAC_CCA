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
            if (!IsValidEmail(email))
            {
                string emailPrefix = email.Split('@')[0];
                // Si el prefijo tiene más de 3 caracteres, se recorta a los primeros 3
                if (emailPrefix.Length > 3)
                {
                    emailPrefix = emailPrefix.Substring(0, 3);
                }

                int randomNumber = random.Next(100, 1000); // 3 dígitos
                password = $"{emailPrefix}{randomNumber}";
            }
            else
            {
                string firstNameInitial = !string.IsNullOrWhiteSpace(nombre) ? nombre.Substring(0, 1).ToUpper() : "X";
                string lastName = !string.IsNullOrWhiteSpace(apellido) ? apellido.Split(' ')[0].ToUpper() : "Y";

                int randomNumber = random.Next(10, 100); 
                password = $"{firstNameInitial}{lastName}{randomNumber}";
            }
            return password;
        }

          public static string GenerateNickName(string nombres, string apellidos)
        {
            Random random = new Random();
            string letters = string.Empty;
            string fullName = $"{nombres}{apellidos}".Replace(" ", "");
            while (letters.Length < 5)
            {
                letters += fullName[random.Next(0, fullName.Length)];
            }
            int randomNumber = random.Next(10, 100); 

            string nickname = $"{letters}{randomNumber}";

            return nickname.ToLower();
        }

        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.IndexOf("@") > 0 && email.IndexOf("@") < email.Length - 1;
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