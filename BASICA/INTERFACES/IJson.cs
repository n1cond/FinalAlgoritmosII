using System.Data;

namespace BASICA
{
    public interface IJson
    {
        /// <summary>
        /// Recibe un JSON y le agrega un par nombre/clave al final
        /// </summary>
        /// <param name="parent">String JSON inicial</param>
        /// <param name="name">Nuevo nombre de clave a agregar</param>
        /// <param name="value">Valor de la clave</param>
        /// <returns>String JSON con nueva clave agregada</returns>
        string addKey(string parent, string name,string value);

        string toJson(DataRow dr);
    }
}