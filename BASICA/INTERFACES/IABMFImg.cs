using System.Web;

namespace BASICA
{
    public interface IABMFImg : IABMF
    {
        string DirBase { get; }
        string Directory { get; }
        string Prefix { get; } //nombre que se le pone al archivo
        string MakeData { get; }
        HttpPostedFile UploadedFile { get; set; }
    }
}