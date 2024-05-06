using System.Data;


namespace BASICA
{
    public abstract class BasicTable : IABMFTable
    {
        #region ABSTRACT
        public abstract string SchemaPath { get; }
        public abstract string Path { get; }          
        public abstract void Add();
        public abstract void Create();
        public abstract void Erase();
        public abstract string Find();
        public abstract string List();
        public abstract void Modify();
        #endregion
        #region METHODS
        public int ID { get; set; }
        public IImage IImg => new Image();
        public DataTable Load()
        {
            DataTable Dt = new DataTable();
            Dt.ReadXmlSchema(SchemaPath);
            Dt.ReadXml(Path);
            return Dt;
        }
        public void Save(DataTable Dt)
        {
            Dt.WriteXmlSchema(SchemaPath);
            Dt.WriteXml(Path);
        }
        #endregion
    }
}