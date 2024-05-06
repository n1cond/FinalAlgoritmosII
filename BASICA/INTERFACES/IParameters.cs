namespace BASICA
{
    /// <summary>
    /// Agrega parámetros a las stored procedures
    /// </summary>
    public interface IParameters
    {
        void AddBit(string name, bool value);
        void AddDateTime(string name, System.DateTime value);
        void AddFloat(string name, double value);
        void AddInt(string name, int value);
        void AddVarchar(string name, int length, string value);
    }
}