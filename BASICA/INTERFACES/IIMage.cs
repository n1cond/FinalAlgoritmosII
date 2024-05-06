using System.Web;

namespace BASICA
{
    public interface IImage:IID
    {
        /// <summary>
        /// Agrega o sustituye una imagen en el sistema web persistente
        /// </summary>
        /// <param name="InputFile">Archivo de imagen</param>
        /// <param name="Data"></param>        
        void Add(HttpPostedFile InputFile, string Data);
        /// <summary>
        /// Elimina un archivo  imagen del sistema web persistente
        /// </summary>
        /// <param name="Data"></param>
        void Erase(string Data);
        /// <summary>
        /// Devuelve la Url de la imagen del objeto
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        string URL(string Data);
    }
}