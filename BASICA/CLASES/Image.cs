using System;
using System.Web;
using System.IO;

namespace BASICA
{
    public class Image : IMakeImg
    {
        public string DirBase { get; set; }

        public string Directory { get; set; }

        public string Path => AppDomain.CurrentDomain.BaseDirectory + DirBase + @"\" + Directory + @"\" + Prefix + ID + "." + Extension;

        private string Extension { get; set; }

        public string Prefix { get; set; }

        public string MakeData { get; set; }
                
        public int ID { get; set; }

        public void Add(HttpPostedFile InputFile, string Data)
        {
            if (InputFile == null || InputFile.FileName == "") return;
            try
            {
                string[] aux = InputFile.FileName.Split('.');
                Erase(Data); //Elimina el archivo que ya existía en el path Data
                Extension = aux[aux.Length - 1];
                InputFile.SaveAs(Path);
            } catch(Exception e)
            {
                throw e;
            }
            
        }

        public void ChangePrefix()
        {
            if(Prefix.EndsWith("_"))
            {
                Prefix = Prefix.Remove(Prefix.Length - 1); return;
            }
            Prefix += "_";
        }

        public void ResetPrefix()
        {
            if (Prefix.EndsWith("_")) ChangePrefix();
        }

        public string FindFilePath()
        {
            string imgFolder = AppDomain.CurrentDomain.BaseDirectory + DirBase + "\\" + Directory + "\\";
            string[] files = new string[0];
            ResetPrefix();
            for (int i = 0; i < 2; i++)
            {
                files = System.IO.Directory.GetFiles(imgFolder, Prefix + ID + ".*");
                if (files.Length > 0) {
                    string file = files[0];
                    string[] aux = file.Split('.');
                    Extension = aux[aux.Length - 1];
                    return files[0];
                }
                ChangePrefix();
            }
            ResetPrefix();
            Extension = "jpg";
            return "";
        }

        public void Erase(string Data)
        {
            string filePath;
            SplitData(Data);
            filePath = FindFilePath();
            if (!string.IsNullOrEmpty(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        public void SplitData(string Data)//Data="23;IMAGENES;USUARIOS;USUARIO"
        {
            string[] Datos = Data.Split(new char[]{';'});
            ID = int.Parse(Datos[0]);
            DirBase = Datos[1];
            Directory = Datos[2];
            Prefix = Datos[3];
        }

        public string URL(string Data)
        {
            SplitData(Data);
            string path = FindFilePath();
            if(path != "") return DirBase + "/" + Directory + "/" + Prefix + ID + "." + Extension;
            return DirBase + "/" + Directory + "/" + Prefix + "default." + Extension;
        }
    }
}