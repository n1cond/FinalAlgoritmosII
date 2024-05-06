namespace BusinessEscuela
{
    internal partial class Singleton : BASICA.ParentSingleton
    {
        static Singleton Instance => new Singleton();
        private Singleton() { }
        public static Singleton GetInstance => Instance;
    }
}
