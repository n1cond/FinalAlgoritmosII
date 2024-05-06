using System.Security.Cryptography;
using System.Text;

namespace BASICA
{
    public class Sha : IHashing
    {
        string IHashing.Hash(string Data)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(Data);
            byte[] result;
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // Convertimos los valores en hexadecimal
                // cuando tiene una cifra hay que rellenarlo con cero
                // para que siempre ocupen dos dígitos.
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            //Devolvemos la cadena con el hash en mayúsculas para que quede más correcto
            return sb.ToString().ToUpper();
        }
    }
    
}