using BASICA;
using System.Web;

namespace BusinessEscuela
{
    public interface ICarrera : IABMFImg
    {
        #region Data Contract
        string Sigla { get; set; }
        string Nombre { get; set; }
        int Duracion { get; set; }
        string Titulo { get; set; }
        string Estado { get; set; }
        #endregion
        HttpPostedFile UploadedPdf { get; set; }

        #region Method Contract
        bool SiglaExists();
        bool NombreExists();
        string List();
        string FindBySigla();
        #endregion
    }
}
