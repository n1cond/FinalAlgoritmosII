using System.Data;
using System.Collections.Generic;

namespace BASICA
{
    public interface IListToTable
    {
        /// <summary>
        /// Convierte una tabla en una lista de objetos de la fila
        /// </summary>
        /// <typeparam name="T">Tipo de dato con constructor genérico</typeparam>
        /// <param name="dt">DataTable de la base de datos</param>
        /// <param name="IGS">Objeto que hereda de IGenericSingleton</param>
        /// <returns></returns>
        IList<T> TableToList<T>(DataTable dt, IGenericSingleton<T> IGS) where T: new();
        /// <summary>
        /// Convierte una tabla en un string JSON
        /// </summary>
        /// <typeparam name="T">Tipo de dato con constructor genérico</typeparam>
        /// <param name="dt">DataTable de la base de datos</param>
        /// <param name="IGS">Objeto que hereda de IGenericSingleton</param>
        /// <returns></returns>
        string TableToJson<T>(DataTable dt, IGenericSingleton<T> IGS) where T : new();
    }
}