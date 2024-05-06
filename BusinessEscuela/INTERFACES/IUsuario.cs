namespace BusinessEscuela
{
    interface IUsuario : BASICA.IABMFImg
    {
        //Data contract
        string Nombre { get; set; }
        int Dni { get; set; }
        string Direccion { get; set; }
        System.DateTime FechaNac { get; set; }
        string Mail { get; set; }
        string Telefono { get; set; }
        string Password { get; set; }

        //Method contract
        bool MailExists();
        bool DniExists();
        string FindByMail();
        string FindByDni();
        string FindByMailAndDni();
        string Login();
        string List();
    }
}
