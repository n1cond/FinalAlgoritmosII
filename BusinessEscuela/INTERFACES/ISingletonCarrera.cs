namespace BusinessEscuela
{ 
    interface ISingletonCarrera : BASICA.IGenericSingleton<Carrera>
    {
        bool SiglaExists(Carrera Data);
        bool NombreExists(Carrera Data);
        string List(Carrera Data);
        string FindBySigla(Carrera Data);
    }
}
