using System;
using System.Web;

namespace BusinessEscuela
{
    public class Usuario : IUsuario
    {
        #region Properties
        internal Singleton S => Singleton.GetInstance;

        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNac { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }

        public string DirBase => "IMAGENES";
        public string Directory => "USUARIOS";
        public string Prefix => "usuario";
        public string MakeData => ID + ";" + DirBase + ";" + Directory + ";" + Prefix;

        public HttpPostedFile UploadedFile { get; set; }
        #endregion
        #region Methods
        public void Add() { S.ISU.Add(this); }

        public bool DniExists() { return S.ISU.DniExists(this); }

        public void Erase() { S.ISU.Erase(this); }

        public string Find() { return S.ISU.Find(this); }

        public string FindByDni() { return S.ISU.FindByDni(this); }

        public string FindByMail() { return S.ISU.FindByMail(this); }

        public string FindByMailAndDni() { return S.ISU.FindByMailAndDni(this); }

        public string List() { return S.ISU.List(); }

        public string Login() { return S.ISU.Login(this); }

        public bool MailExists() { return S.ISU.MailExists(this); }

        public void Modify() { S.ISU.Modify(this); }
        #endregion
    }
}
