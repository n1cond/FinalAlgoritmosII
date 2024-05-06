namespace BusinessEscuela
{
    interface ISingletonUsuarioRol : BASICA.IGenericSingleton<UsuarioRol>
    {
        string ListByUsuario(UsuarioRol Data);
    }
}
