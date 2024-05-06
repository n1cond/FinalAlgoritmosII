using System.Data;

namespace BASICA
{
    public interface IABMFTable:IABMF
    {
        string List();//produce una lista e json de la tabla
        string SchemaPath { get; }//obtine la direccion del servidor
        string Path { get; }
        void Create();//crea la tabla
        void Save(DataTable Dt);
        DataTable Load();
        IImage IImg { get;}
    }
}