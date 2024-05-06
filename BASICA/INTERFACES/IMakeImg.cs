namespace BASICA
{
    public interface IMakeImg : IImage
    {
        string DirBase { get; }
        string Directory { get; }
        string Path { get; }
        string Prefix { get; } //nombre que se le pone al archivo
        void ResetPrefix();
        void ChangePrefix();
        void SplitData(string Data);
    }
}