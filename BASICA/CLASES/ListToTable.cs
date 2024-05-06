using System.Collections.Generic;
using System.Data;

namespace BASICA
{
    class ListToTable : IListToTable
    {
        public string TableToJson<T>(DataTable dt, IGenericSingleton<T> IGS) where T : new()
        {
            if (dt.Rows.Count == 0) return "[]";
            string json = "[";
            foreach (DataRow dr in dt.Rows)
            {
                json += IGS.MakeJson(dr, new T()) + ",";
            }
            return json.Remove(json.Length - 1) + "]";
        }

        public IList<T> TableToList<T>(DataTable dt, IGenericSingleton<T> IGS) where T : new()
        {
            IList<T> IL = new List<T>();
            foreach(DataRow dr in dt.Rows)
            {
                T t = new T();
                IGS.MakeJson(dr, t);
                IL.Add(t);
            }
            return IL;
        }
    }
}
