namespace BusinessEscuela
{
    interface IUsuarioRol : BASICA.IABMF
    {
        Usuario Usuario { get; set; }
        string Rol { get; set; }
        string ListByUsuario();
    }
}
