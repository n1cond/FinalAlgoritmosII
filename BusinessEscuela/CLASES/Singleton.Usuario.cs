using System;
using BASICA;
using System.Data;

namespace BusinessEscuela
{
    internal partial class Singleton : ISingletonUsuario
    {
        public ISingletonUsuario ISU => this;

        void IGenericSingleton<Usuario>.Add(Usuario Data)
        {
            if (Data.DniExists()) throw new Exception("Existe otro usuario con el mismo DNI.");
            if (Data.MailExists()) throw new Exception("Existe otro usuario con el mismo mail.");
            Data.Password = IHashing.Hash(Data.Password);
            IConnection.CreateCommand("Usuarios_Insert", "Usuario");
            IConnection.AddVarchar("Nombre", 30, Data.Nombre);
            IConnection.AddInt("Dni", Data.Dni);
            IConnection.AddVarchar("Direccion", 50, Data.Direccion);
            IConnection.AddDateTime("FechaNac", Data.FechaNac);
            IConnection.AddVarchar("Mail", 40, Data.Mail);
            IConnection.AddVarchar("Telefono", 15, Data.Telefono);
            IConnection.AddVarchar("Password", 40, Data.Password);
            Data.ID = IConnection.Insert();
        }

        bool ISingletonUsuario.DniExists(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_DniExists", "DNI");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddInt("Dni", Data.Dni);
            return IConnection.Exists();
        }

        bool ISingletonUsuario.MailExists(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_MailExists", "Mail");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddVarchar("Mail", 40, Data.Mail);
            return IConnection.Exists();
        }

        void IGenericSingleton<Usuario>.Erase(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_Delete", "Usuario");
            IConnection.AddInt("ID", Data.ID);
            IConnection.Delete();
            IImage.Erase(Data.MakeData);
        }

        string IGenericSingleton<Usuario>.Find(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_Find", "Usuario");
            IConnection.AddInt("ID", Data.ID);
            DataRow dr = IConnection.Find();
            return ISU.MakeJson(dr, Data);
        }

        string ISingletonUsuario.FindByDni(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_FindByDni", "Usuario");
            IConnection.AddInt("Dni", Data.Dni);
            DataRow dr = IConnection.Find();
            return ISU.MakeJson(dr, Data);
        }

        string ISingletonUsuario.FindByMail(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_FindByMail", "Usuario");
            IConnection.AddVarchar("Mail", 40, Data.Mail);
            DataRow dr = IConnection.Find();
            return ISU.MakeJson(dr, Data);
        }

        string ISingletonUsuario.FindByMailAndDni(Usuario Data)
        {
            IConnection.CreateCommand("Usuarios_FindByMailAndDni", "Usuario");
            IConnection.AddVarchar("Mail", 40, Data.Mail);
            IConnection.AddInt("Dni", Data.Dni);
            DataRow dr = IConnection.Find();
            return ISU.MakeJson(dr, Data);
        }

        string ISingletonUsuario.List()
        {
            IConnection.CreateCommand("Usuarios_List", "Usuarios");
            DataTable dt = IConnection.List();
            return IListToTable.TableToJson(dt, ISU);
        }

        string ISingletonUsuario.Login(Usuario Data)
        { //El password hasta acá es texto plano
            IConnection.CreateCommand("Usuarios_Login", "Usuario");
            IConnection.AddVarchar("Mail", 40, Data.Mail);
            Data.Password = IHashing.Hash(Data.Password);
            IConnection.AddVarchar("Password", 40, Data.Password);
            DataRow dr;
            try
            {
                dr = IConnection.Find();
            } catch(Exception e)
            {
                throw e;
            }
            return ISU.MakeJson(dr, Data);
        }

        string IGenericSingleton<Usuario>.MakeJson(DataRow Dr, Usuario Data)
        {
            Data.ID = int.Parse(Dr.ItemArray[0].ToString());
            Data.Nombre = Dr.ItemArray[1].ToString();
            Data.Dni = int.Parse(Dr.ItemArray[2].ToString());
            Data.Direccion = Dr.ItemArray[3].ToString();
            Data.FechaNac = DateTime.Parse(Dr.ItemArray[4].ToString());
            Data.Mail = Dr.ItemArray[5].ToString();
            Data.Telefono = Dr.ItemArray[6].ToString();

            string json = "{}";
            json = IJson.addKey(json, "ID", Data.ID.ToString());
            json = IJson.addKey(json, "Nombre", Data.Nombre);
            json = IJson.addKey(json, "Dni", Data.Dni.ToString());
            json = IJson.addKey(json, "Direccion", Data.Direccion);
            json = IJson.addKey(json, "FechaNac", Data.FechaNac.ToString("yyyy-MM-dd"));
            json = IJson.addKey(json, "Mail", Data.Mail);
            json = IJson.addKey(json, "Telefono", Data.Telefono);
            json = IJson.addKey(json, "FotoURL", IImage.URL(Data.MakeData));
            return json;
        }

        void IGenericSingleton<Usuario>.Modify(Usuario Data)
        {
            if (Data.DniExists()) throw new Exception("Existe otro usuario con el mismo DNI.");
            if (Data.MailExists()) throw new Exception("Existe otro usuario con el mismo mail.");
            if (Data.Password != "") Data.Password = IHashing.Hash(Data.Password);
            IConnection.CreateCommand("Usuarios_Update", "Usuario");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddVarchar("Nombre", 30, Data.Nombre);
            IConnection.AddVarchar("Direccion", 50, Data.Direccion);
            IConnection.AddDateTime("FechaNac", Data.FechaNac);
            IConnection.AddVarchar("Telefono", 15, Data.Telefono);
            IConnection.AddVarchar("Password", 40, Data.Password);
            IConnection.Update();
            if(Data.UploadedFile != null) IImage.Add(Data.UploadedFile, Data.MakeData);
        }
    }
}
