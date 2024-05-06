const DEFAULTUSER = {
    ID: "",
    Nombre: "",
    Dni: "",
    Direccion: "",
    FechaNac: "",
    Mail: "",
    Password: "",
    Telefono: "",
    Roles: [],
    FotoURL: ""
};

const ROLES = [
    "ADMINISTRADOR",
    "DIRECTOR DE ESTUDIOS",
    "PROFESOR",
    "PRECEPTOR",
    "ALUMNO",
    "EXCLUIDO"
];

//VARIABLES GLOBALES
//Rol: string ROLES[x]
//MainUser: formato DEFAULTUSER
let Rol, MainUser, Users;


const $$ClassUser = function () { //Métodos para hacer ABMC de usuarios y login

    //FUNCIONES
    const Load = () => {
        $a.loadUsers();
    }

    const Clear = () => { Users = null; }

    //METODOS
    this.add = function (user) {
        let data = "&Nombre=" + user.Nombre;
        data += "&Dni=" + user.Dni;
        data += "&Direccion=" + user.Direccion;
        data += "&FechaNac=" + user.FechaNac;
        data += "&Mail=" + user.Mail;
        data += "&Telefono=" + user.Telefono;
        data += "&Roles=" + user.Roles.toString();
        
        let respuesta = $a.consulta("accion=AGREGARUSUARIO" + data);
        if (respuesta !== "OK") throw (respuesta);
    };

    this.erase = function (user) {
        $a.consulta("accion=ELIMINARUSUARIO&ID=" + user.ID);
    };

    this.modify = function (user) {
        let data = "&ID=" + user.ID;
        data +="&Nombre=" + user.Nombre;
        data += "&Direccion=" + user.Direccion;
        data += "&FechaNac=" + user.FechaNac;
        data += "&Telefono=" + user.Telefono;
        if(user.Roles != null) data += "&Roles=" + user.Roles.toString();
        if (user.Password != null) data += "&Password=" + user.Password;

        let respuesta = $a.consulta("accion=MODIFICARUSUARIO" + data);
        if (respuesta.startsWith("Error")) throw (respuesta);
        //Si el usuario modificado es el MainUser, tengo que actualizarlo en el cliente también
        if (MainUser !== undefined) {
            if (user.ID === MainUser.ID) {
                //Si no hubo error en la consulta, cargo el usuario tal cual se guardó en la base de datos
                MainUser = JSON.parse(respuesta);
                //Los roles se deben cargar aparte
                MainUser.roles = $a.consulta("accion=CARGARROLESUSUARIO&ID=" + MainUser.ID);
                MainUser.Rol = MainUser.roles[0];
            }
        }
    };

    this.listUsers = function () {
        //Crea una lista de usuarios
        Load();
        $tablas.listaUsuarios(Users);
        Clear();
    };

    this.findUserByMail = function (mail) {
        let res = $a.consulta("accion=ENCONTRARUSUARIO&Mail=" + mail);
        if (res.startsWith("Error")) throw (res);
        return JSON.parse(res);
        throw ("No se encontró un usuario con este mail.");
    };

    this.findUserByDni = function (dni) {
        let res = $a.consulta("accion=ENCONTRARUSUARIO&Dni=" + dni);
        if (res.startsWith("Error")) throw (res);
        return JSON.parse(res);
        throw ("No se encontró un usuario con este DNI.");
    };

    this.findUserByMailAndDni = function (user) {
        //Encuentro al usuario por mail y compruebo que coincida el DNI
        let res = $a.consulta("accion=ENCONTRARUSUARIO&Dni=" + user.Dni + "&Mail=" + user.Mail);
        if (res.startsWith("Error")) throw (res);
        return JSON.parse(res);
        throw ("No se encontró un usuario con este mail y DNI.");
    };
    
};
const $cu = new $$ClassUser();

//MÉTODOS USADOS POR CIERTOS ROLES DE USUARIO
const $$UserFunctions = function () {

    //Administrador
    this.addUser = function () {
        $uf.addUser();
    };

    this.modifyUserByDni = function () {
        $uf.modifyUserByDni();
    };

    this.modifyUserByMail = function () {
        $uf.modifyUserByMail();
    };

}
const $uFunctions = new $$UserFunctions();

