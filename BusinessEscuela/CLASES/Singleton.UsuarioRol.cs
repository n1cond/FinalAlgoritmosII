using System;
using System.Data;
using BASICA;

namespace BusinessEscuela
{
    internal partial class Singleton : ISingletonUsuarioRol
    {
        public ISingletonUsuarioRol ISUR => this;

        void IGenericSingleton<UsuarioRol>.Add(UsuarioRol Data)
        {
            IConnection.CreateCommand("UsuariosRoles_Insert", "Rol de usuario");
            IConnection.AddInt("IDUsuario", Data.Usuario.ID);
            IConnection.AddVarchar("Rol", 20, Data.Rol);
            Data.ID = IConnection.Insert();
        }

        void IGenericSingleton<UsuarioRol>.Erase(UsuarioRol Data)
        {
            IConnection.CreateCommand("UsuariosRoles_Delete", "Rol de usuario");
            IConnection.AddInt("IDUsuario", Data.Usuario.ID);
            IConnection.AddVarchar("Rol", 20, Data.Rol);
            IConnection.Delete();
        }

        string IGenericSingleton<UsuarioRol>.Find(UsuarioRol Data)
        {
            IConnection.CreateCommand("UsuariosRoles_Find", "Rol de usuario");
            IConnection.AddInt("IDUsuario", Data.Usuario.ID);
            IConnection.AddVarchar("Rol", 20, Data.Rol);
            DataRow dr = IConnection.Find();
            return IJson.toJson(dr);
        }

        string ISingletonUsuarioRol.ListByUsuario(UsuarioRol Data)
        {
            IConnection.CreateCommand("UsuariosRoles_ListByUsuario", "Rol de usuario");
            IConnection.AddInt("IDUsuario", Data.Usuario.ID);
            DataTable dt = IConnection.List();
            string roles = IListToTable.TableToJson(dt, ISUR);
            return roles;
        }

        string IGenericSingleton<UsuarioRol>.MakeJson(DataRow Dr, UsuarioRol Data)
        {
            return "\"" + Dr["Rol"] + "\"";
        }

        void IGenericSingleton<UsuarioRol>.Modify(UsuarioRol Data)
        {
            IConnection.CreateCommand("UsuariosRoles_Update", "Rol de usuario");
            IConnection.AddInt("ID", Data.ID);
            IConnection.AddInt("IDUsuario", Data.Usuario.ID);
            IConnection.AddVarchar("Rol", 20, Data.Rol);
            IConnection.Update();
        }
    }
}
