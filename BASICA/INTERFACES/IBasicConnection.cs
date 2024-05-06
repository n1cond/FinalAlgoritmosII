namespace BASICA
{
    public interface IBasicConnection
    {
        string AddData { get; set; }

        void OpenConnection();
    }
}