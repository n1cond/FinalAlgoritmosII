using System;
using System.Data;
using BASICA;

namespace BusinessEscuela
{
    partial class Singleton : ISingletonCarrera
    {
        public ISingletonCarrera ISC => this;

        void IGenericSingleton<Carrera>.Add(Carrera Data)
        {
            if (Data.NombreExists()) throw new Exception("Existe otra carrera con el mismo nombre.");
            if (Data.SiglaExists()) throw new Exception("Existe otra carrera con la misma sigla.");
            IConnection.CreateCommand("Carreras_Insert", "Carrera");
            IConnection.AddVarchar("Sigla", 10, Data.Sigla);
            IConnection.AddVarchar("Nombre", 60, Data.Nombre);
            IConnection.AddInt("Duracion", Data.Duracion);
            IConnection.AddVarchar("Titulo", 60, Data.Titulo);
            IConnection.AddVarchar("Estado", 10, Data.Estado);
            Data.ID = IConnection.Insert();
            IImage.Add(Data.UploadedFile, Data.MakeData);
            IImage.Add(Data.UploadedPdf, Data.MakeDataPdf);
        }

        void IGenericSingleton<Carrera>.Erase(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_Delete", "Carrera");
            IConnection.AddInt("ID", Data.ID);
            IConnection.Delete();
            IImage.Erase(Data.MakeData);
            IImage.Erase(Data.MakeDataPdf);
        }

        string IGenericSingleton<Carrera>.Find(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_Find", "Carrera");
            IConnection.AddInt("ID", Data.ID);
            DataRow dr = IConnection.Find();
            return ISC.MakeJson(dr, Data);
        }

        string ISingletonCarrera.FindBySigla(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_FindBySigla", "Carrera");
            IConnection.AddVarchar("Sigla", 10, Data.Sigla);
            DataRow dr = IConnection.Find();
            return ISC.MakeJson(dr, Data);
        }

        string ISingletonCarrera.List(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_List", "Carreras");
            IConnection.AddVarchar("Estado", 10, Data.Estado);
            DataTable dt = IConnection.List();
            return IListToTable.TableToJson(dt, ISC);
        }

        string IGenericSingleton<Carrera>.MakeJson(DataRow Dr, Carrera Data)
        {
            Data.ID = int.Parse(Dr.ItemArray[0].ToString());
            Data.Sigla = Dr.ItemArray[1].ToString();
            Data.Nombre = Dr.ItemArray[2].ToString();
            Data.Duracion = int.Parse(Dr.ItemArray[3].ToString());
            Data.Titulo = Dr.ItemArray[4].ToString();
            Data.Estado = Dr.ItemArray[5].ToString();

            string json = "{}";
            json = IJson.addKey(json, "ID", Data.ID.ToString());
            json = IJson.addKey(json, "Sigla", Data.Sigla);
            json = IJson.addKey(json, "Nombre", Data.Nombre);
            json = IJson.addKey(json, "Duracion", Data.Duracion.ToString());
            json = IJson.addKey(json, "Titulo", Data.Titulo);
            json = IJson.addKey(json, "Estado", Data.Estado);
            json = IJson.addKey(json, "FotoURL", IImage.URL(Data.MakeData));
            json = IJson.addKey(json, "PdfURL", IImage.URL(Data.MakeDataPdf));
            return json;
        }

        void IGenericSingleton<Carrera>.Modify(Carrera Data)
        {
            if (Data.NombreExists()) throw new Exception("Existe otra carrera con el mismo nombre.");
            if (Data.SiglaExists()) throw new Exception("Existe otra carrera con la misma sigla.");
            IConnection.CreateCommand("Carreras_Update", "Carrera");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddVarchar("Sigla", 10, Data.Sigla);
            IConnection.AddVarchar("Nombre", 60, Data.Nombre);
            IConnection.AddInt("Duracion", Data.Duracion);
            IConnection.AddVarchar("Titulo", 60, Data.Titulo);
            IConnection.AddVarchar("Estado", 10, Data.Estado);
            IConnection.Update();
            if (Data.UploadedFile != null) IImage.Add(Data.UploadedFile, Data.MakeData);
            if (Data.UploadedPdf != null) IImage.Add(Data.UploadedPdf, Data.MakeDataPdf);
        }

        bool ISingletonCarrera.NombreExists(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_NombreExists", "Nombre");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddVarchar("Nombre", 60, Data.Nombre);
            return IConnection.Exists();
        }

        bool ISingletonCarrera.SiglaExists(Carrera Data)
        {
            IConnection.CreateCommand("Carreras_SiglaExists", "Sigla");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddVarchar("Sigla", 10, Data.Sigla);
            return IConnection.Exists();
        }
    }
}
