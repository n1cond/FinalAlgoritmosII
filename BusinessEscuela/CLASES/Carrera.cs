using System.Web;

namespace BusinessEscuela
{
    public class Carrera : ICarrera
    {
        #region Properties
        internal Singleton S => Singleton.GetInstance;

        public string Sigla { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
        public HttpPostedFile UploadedPdf { get; set; }

        public string DirBase => "IMAGENES";
        public string Directory => "CARRERAS";
        public string Prefix => "";
        public string DirBasePdf => "DOCUMENTOS";
        public string DirectoryPdf => "CARRERAS";
        public string MakeData => ID + ";" + DirBase + ";" + Directory + ";" + Prefix;
        public string MakeDataPdf => ID + ";" + DirBasePdf + ";" + DirectoryPdf + ";" + Prefix;

        public HttpPostedFile UploadedFile { get; set; }
        public int ID { get; set; }
        #endregion
        #region Methods
        public void Add() { S.ISC.Add(this); }

        public void Erase() { S.ISC.Erase(this); }

        public string Find() { return S.ISC.Find(this); }

        public string FindBySigla() { return S.ISC.FindBySigla(this); }

        public string List() { return S.ISC.List(this); }

        public void Modify() { S.ISC.Modify(this); }

        public bool NombreExists() { return S.ISC.NombreExists(this); }

        public bool SiglaExists() { return S.ISC.SiglaExists(this); }
        #endregion
    }
}
