const $$Nav = function () {
    //FUNCIONES
    //Generar links
    const LinksBase = function () {
        Nav.innerHTML = "";
        ul = $dc.ul(Nav);
        divLogin = $dc.div(Nav);
        divLogin.id = "divLogin";
        $dn.button("INICIO", $ba.home);
        $dn.dropdownButton("CONFIGURACIÓN", ["modificar estilo"], [$ba.cambiarEstilo]);
    };
    const LinksUsers = function () {
        LinksBase();
        $dn.button("PERFIL", $ba.perfilUsuario);
    };
    const LinksDefault = function () {
        LinksBase();
        $dn.button("noticias", $ba.listNoticias);
        $dn.button("carreras", $ba.listCarreras);
    };
    //Crear botones
    const ButtonLogin = function () {
        let a = $dc.button(divLogin, "Iniciar sesión", $ba.buttonLogin);
        a.className = "Login";
        //let li = $dn.button("Iniciar sesión", $ba.buttonLogin);
        //li.className = "Login";
    };
    const ButtonLogout = function () {
        let a = $dc.button(divLogin, "Cerrar sesión", $ba.buttonLogout);
        a.className = "Login";
        //let li = $dn.button("Cerrar sesión", $ba.buttonLogout);
        //li.className = "Login";
    };

    //PROCEDIMIENTOS
    //Cargar botones según rol de usuario
    this.init = function () {
        Nav.innerHTML = "";
        if (Rol === undefined) {
            $n.defaultUser(); return;
        }
        switch (Rol) {
            case "ADMINISTRADOR":
                $n.administrador(); break;
            case "DIRECTOR DE ESTUDIOS":
                $n.directorDeEstudios(); break;
            case "PROFESOR":
                $n.profesor(); break;
            case "PRECEPTOR":
                $n.preceptor(); break;
            case "ALUMNO":
                $n.alumno(); break;
            case "INSCRIPTO":
                $n.alumno(); break;
            case "EXCLUIDO":
                $n.excluido(); break;
            default:
                $n.defaultUser();
        }
    };
    //Botones específicos de cada rol
    this.defaultUser = function () { LinksBase(); LinksDefault(); ButtonLogin(); };
    this.administrador = function () {
        LinksUsers();
        $dn.dropdownButton("FUNCIONES", [
            "ALTA USUARIO",
            "MOD USUARIO POR MAIL",
            "MOD USUARIO POR DNI",
            "ABM. CARRERAS"
        ], [
            $ba.addUser,
            $ba.modUserByMail,
            $ba.modUserByDni,
            $ba.abmCarreras
        ]);
        ButtonLogout();
    };
    this.directorDeEstudios = function () {
        LinksUsers();
        $dn.dropdownButton("FUNCIONES", [
            "AB MATERIAS",
            "ab correlativas"
        ], [
            "",
            ""
        ]);
        ButtonLogout();
    };
    this.profesor = function () {
        LinksUsers();
        $dn.dropdownButton("FUNCIONES", [
            "mis cursos",
            "exámenes"
        ], [
            "",
            ""
            ]);
        ButtonLogout();
    };
    this.preceptor = function () {
        LinksUsers();
        $dn.dropdownButton("FUNCIONES", [
            "alta alumno",
            "verificar alumno"
        ], [
                "",
                ""
            ]);
        ButtonLogout();
    };
    this.alumno = function () {
        LinksUsers();
        $dn.dropdownButton("FUNCIONES", [
            "mis cursos",
            "inscribirme"
        ], [
                "",
                ""
            ]);
        ButtonLogout();
    };
    this.excluido = function () { LinksBase(); LinksDefault(); ButtonLogout(); };
};
const $n = new $$Nav();

//////////////////////////////////////////////////////////////////////////////////////
//                      
//                                CLASE BUTTON ACTIONS
//              Guarda todas las funciones de acción de los botones del nav
//
//////////////////////////////////////////////////////////////////////////////////////

const $$ButtonActions = function () {
    //Botones básicos
    this.home = Home;
    this.cambiarEstilo = modificarLink;

    //LOGIN y LOGOUT
    this.buttonLogin = function () {
        $uf.formLogin();
    };
    this.buttonLogout = function () {
        $uf.formLogout();
    };

    //Botones para USUARIO NO LOGUEADO/EXCLUIDO
    this.listCarreras = function () { };
    this.listNoticias = function () { };

    //Botones para TODOS LOS USUARIOS
    this.perfilUsuario = function () {
        $uf.formProfile();
    };

    //Botones ADMINISTRADOR
    this.abmCarreras = function () {
        $navegacion.abmCarreras();
    };
    this.addUser = function () {
        $navegacion.abmLista();
    };
    this.modUserByMail = function () { $uf.formFindBy("Mail"); };
    this.modUserByDni = function () { $uf.formFindBy("Dni"); };
};
const $ba = new $$ButtonActions();