using System;
using System.Web;
using BASICA;
using BusinessEscuela;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender,EventArgs e)
    {
        if(Request["accion"] != null)
        {
            switch(Request["accion"])
            {
                case "LOGIN": Login(); break;
                case "CARGARUSUARIOS": CargarUsuarios(); break;
                case "AGREGARUSUARIO": AgregarUsuario(); break;
                case "MODIFICARUSUARIO": ModificarUsuario(); break;
                case "CARGARROLESUSUARIO": CargarRolesUsuario(); break;
                case "ELIMINARUSUARIO": EliminarUsuario(); break;
                case "ENCONTRARUSUARIO": EncontrarUsuario(); break;
                case "AGREGARCARRERA": AgregarCarrera(); break;
                case "MODIFICARCARRERA": ModificarCarrera(); break;
                case "ELIMINARCARRERA": EliminarCarrera(); break;
                case "ENCONTRARCARRERA": EncontrarCarrera(); break;
                case "CARGARCARRERAS": CargarCarreras(); break;
                default: return;
            }
        }
    }

    private void CargarCarreras()
    {
        try
        {
            Response.Write(new Carrera() { Estado = "ACTIVO" }.List());
        } catch (Exception e)
        {
            Response.Write("Error al cargar carreras: " + e.Message);
        }
    }

    private void EncontrarCarrera()
    {
        throw new NotImplementedException();
    }

    private void EliminarCarrera()
    {
        try
        {
            new Carrera() { ID = int.Parse(Request["ID"]) }.Erase();
            Response.Write("OK");
        } catch (Exception e)
        {
            Response.Write("Error al eliminar carrera: " + e.Message);
        }
    }

    private void ModificarCarrera()
    {
        try
        {
            Carrera carrera = new Carrera
            {
                ID = int.Parse(Request["ID"]),
                Sigla = Request["Sigla"],
                Nombre = Request["Nombre"],
                Duracion = int.Parse(Request["Duracion"]),
                Titulo = Request["Titulo"],
                Estado = Request["Estado"]
            };
            if (Request.Files.Count > 0)
            {
                carrera.UploadedFile = Request.Files[0];
                carrera.UploadedPdf = Request.Files[1];
            }
            carrera.Modify();
            Response.Write(carrera.Find());
        }
        catch (Exception e)
        {
            Response.Write("Error al modificar carrera: " + e.Message);
        }
    }

    private void AgregarCarrera()
    {
        try
        {
            Carrera carrera = new Carrera
            {
                Sigla = Request["Sigla"],
                Nombre = Request["Nombre"],
                Titulo = Request["Titulo"],
                Duracion = int.Parse(Request["Duracion"]),
                Estado = Request["Estado"]
            };

            carrera.UploadedFile = Request.Files[0];
            carrera.UploadedPdf = Request.Files[1];

            carrera.Add();
            Response.Write("OK");
        } catch(Exception e)
        {
            Response.Write("Error al agregar carrera: " + e.Message);
        }
    }

    private void EncontrarUsuario()
    {
        try
        {
            Usuario usuario = new Usuario();
            if (Request["Mail"] != null && Request["Dni"] != null)
            { // Si hay mail y Dni busco por ambas
                usuario.Mail = Request["Mail"];
                usuario.Dni = int.Parse(Request["Dni"]);
                Response.Write(usuario.FindByMailAndDni());
                return;
            }
            if (Request["Mail"] != null)
            { // Si hay mail pero no Dni busco por mail
                usuario.Mail = Request["Mail"];
                Response.Write(usuario.FindByMail());
                return;
            } // Si no hay mail sólo hay Dni, busco por Dni
            usuario.Dni = int.Parse(Request["Dni"]);
            Response.Write(usuario.FindByDni());
            return;
        } catch (Exception e)
        {
            Response.Write("Error: " + e.Message);
        }
    }

    private void EliminarUsuario()
    {
        try
        {
            Usuario usuario = new Usuario()
            {
                ID = int.Parse(Request["ID"])
            };
            usuario.Erase();
        } catch(Exception e)
        {
            Response.Write("Error al eliminar usuario: " + e.Message);
        }
        
    }

    private void AgregarUsuario()
    {
        try
        {
            Usuario usuario = new Usuario {
                Nombre = Request["Nombre"],
                Dni = int.Parse(Request["Dni"]),
                Direccion = Request["Direccion"],
                FechaNac = DateTime.Parse(Request["FechaNac"]),
                Mail = Request["Mail"],
                Telefono = Request["Telefono"],
                Password = Request["Dni"]
            };
            usuario.Add();
            //Recuperar el array de roles
            string roles = Request["Roles"];
            //roles = roles.Remove(roles.Length - 1).Substring(1);
            string[] rolesArray = roles.Split(',');

            UsuarioRol usuarioRol = new UsuarioRol
            {
                Usuario = usuario
            };

            foreach(string rol in rolesArray)
            {
                usuarioRol.Rol = rol;
                usuarioRol.Add();
            }
            Response.Write("OK");
        } catch(Exception e)
        {
            Response.Write("Error al agregar usuario: " + e.Message);
        }
        
    }

    private void CargarRolesUsuario()
    {
        Usuario usuario = new Usuario { ID = int.Parse(Request["ID"]) };
        UsuarioRol usuarioRol = new UsuarioRol { Usuario = usuario };
        Response.Write(usuarioRol.ListByUsuario());
    }

    private void ModificarUsuario()
    {
        try
        {
            Usuario usuario = new Usuario();
            usuario.ID = int.Parse(Request["ID"]);
            usuario.Find(); //Este método carga al objeto usuario los valores que tiene guardados en la base de datos

            usuario.Nombre = Request["Nombre"];
            usuario.Direccion = Request["Direccion"];
            usuario.FechaNac = DateTime.Parse(Request["FechaNac"]);
            usuario.Telefono = Request["Telefono"];
            if (Request["Password"] != null)
                usuario.Password = Request["Password"];
            else
                usuario.Password = "";
            if (Request.Files.Count > 0)
            usuario.UploadedFile = Request.Files[0];

            usuario.Modify();

            if (Request["Roles"] != null) _modificarRoles(usuario);

            Response.Write(usuario.Find());
        } catch(Exception e)
        {
            Response.Write("Error al modificar usuario: " + e.Message);
        }
    }

    private void _modificarRoles(Usuario usuario)
    {
        //MODIFICACIÓN DE ROLES
        UsuarioRol usuarioRol = new UsuarioRol { Usuario = usuario };
        //Para modificar los roles, elimino los que existen y creo los nuevos
        //Roles nuevos
        string[] rolesArray = Request["Roles"].Split(',');
        //Roles viejos
        string jsonRoles = usuarioRol.ListByUsuario();
        string[] rolesAnteriores = jsonRoles.Remove(jsonRoles.Length - 1).Substring(1).Split(',');

        for (int i = 0; i < rolesAnteriores.Length; i++)
        {
            //Cada rol en rolesAnteriores viene entre comillas, así que hay que eliminarlas
            usuarioRol.Rol = rolesAnteriores[i].Remove(rolesAnteriores[i].Length - 1).Substring(1);
            usuarioRol.Erase();
        }

        for (int i = 0; i < rolesArray.Length; i++)
        {
            usuarioRol.Rol = rolesArray[i];
            usuarioRol.Add();
        }
    }

    private void CargarUsuarios()
    {
        try
        {
            Response.Write(new Usuario().List());
        } catch (Exception e)
        {
            throw new Exception("Error al cargar los usuarios: " + e.Message);
        }
    }

    private void Login()
    {
        try
        {
            Usuario usuario = new Usuario
            {
                Mail = Request["Mail"],
                Password = Request["Password"]
            };
            usuario.Login();
            UsuarioRol usuarioRol = new UsuarioRol { Usuario = usuario };
            string jsonUsuario = usuario.FindByMail();
            Json parser = new Json();
            jsonUsuario = parser.addKey(
                jsonUsuario, "Roles", usuarioRol.ListByUsuario());
            Response.Write(jsonUsuario);
        } catch(Exception e)
        {
            if (e.Message == "No fue posible buscar Usuario: " +
                "No hay ninguna fila en la posición 0.")
            {
                Response.Write("Usuario o contraseña incorrectos. Intente nuevamente.");
                return;
            }
            Response.Write("Error al iniciar sesión: " + e.Message);
        }
        
    }
}