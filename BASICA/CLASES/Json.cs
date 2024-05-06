using System.Data;

namespace BASICA
{
    public class Json : IJson
    {
        private string Quotes(string data) { return $"\"{data}\""; }

        public string addKey(string parent, string name, string value)
        {
            string Sufix = "", NewItem;
            name = Quotes(name) + ":";
            if (!value.StartsWith("[") && !value.StartsWith("{")) value = Quotes(value);
            if (parent.StartsWith("{")) { Sufix = "}"; }
            if (parent.StartsWith("[")) { Sufix = "]"; }
            NewItem = name + value + Sufix;
            if (parent != "{}" && parent != "[]") { NewItem = "," + NewItem; }
            return parent.Remove(parent.Length - 1) + NewItem;
        }

        public string toJson(DataRow dr)
        {
            string json = "{";
            for(int i = 0; i < dr.ItemArray.Length; i++)
            {
                string key = Quotes(dr.Table.Columns[i].ColumnName) + ":" + Quotes(dr[i].ToString()) + ",";
                json += key;
            }
            json = json.Remove(json.Length - 1) + "}";
            return json;
        }
    }
}