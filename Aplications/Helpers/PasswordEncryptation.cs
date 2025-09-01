using System.Security.Cryptography;
using System.Text;

namespace RegistroLegal.Core.Aplications.Helpers
{
    public static class PasswordEncryptation
    {
        public static string ComputeSha256Hash(string password) 
        {
            //Primero crea el algoritmo de hash seguro - SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes =  sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convertimos ese arreglo de byte a string para guardarlo en bd.

                StringBuilder sb = new StringBuilder();
                foreach (var item in bytes)
                {
                    sb.Append(item.ToString("x2"));   
                }

                return sb.ToString();

            }
        
        } 
    }
}
