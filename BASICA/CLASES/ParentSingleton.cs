namespace BASICA
{
    public class ParentSingleton
    {
        public IHashing IHashing => new Sha();
        public IImage IImage => new Image();
        public IJson IJson => new Json();
        public IConnection IConnection => Connection.GetInstance();
        public IListToTable IListToTable => new ListToTable();
    }
}