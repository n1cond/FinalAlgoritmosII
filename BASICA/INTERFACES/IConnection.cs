namespace BASICA
{
    public interface IConnection : IParameters
    {
        /// <summary>
        /// Crea una conexión
        /// </summary>
        /// <param name="storedProc">Nombre del stored procedure</param>
        /// <param name="data">Datos auxiliares</param>
        void CreateCommand(string storedProc, string data);
        /// <summary>
        /// Elimina un registro de la tabla
        /// </summary>
        void Delete();
        /// <summary>
        /// Inserta un objeto
        /// </summary>
        /// <returns>ID del registro</returns>
        int Insert();
        /// <summary>
        /// Devuelve el registro
        /// </summary>
        /// <returns>DataRow del registro</returns>
        System.Data.DataRow Find();
        /// <summary>
        /// Lista todos los registros
        /// </summary>
        /// <returns>DataTable de registros</returns>
        System.Data.DataTable List();
        /// <summary>
        /// Verifica si existe
        /// </summary>
        /// <returns>Si existe o no</returns>
        bool Exists();
        /// <summary>
        /// Modifica el objeto
        /// </summary>
        void Update();
    }
}
