using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace BASICA
{
    /// <summary>
    /// Esta clase genera una conexión a una base de datos
    /// y ejecuta procedimientos almacenados.
    /// Tolera una sola conexión en un entorno desconectado.
    /// Abre y cierra la conexión en cada consulta.
    /// La cadena de conexión se puede situar de dos maneras:
    /// La primera, si es un sitio web, es a través de Web.Config
    /// con el nombre "My Connection",
    /// La segunda, en caso de no tener Web.Config
    /// se busca en la carpeta Bin el archivo Connection.txt
    /// y utiliza el patrón singleton para obtener una sola instancia de él
    /// </summary>
    public class Connection: IBasicConnection, IConnection
    {
        #region Variables
        string PathConfig = AppDomain.CurrentDomain.BaseDirectory + "Web.Config";
        string PathConnection = AppDomain.CurrentDomain.BaseDirectory + @"..\Conexion.txt";
        SqlConnection MyConnection = new SqlConnection();
        SqlCommand MyCommand;
        #endregion

        #region IBasicConnection
        public string AddData { get; set; }

        public void OpenConnection()
        {
            if (MyConnection.State != ConnectionState.Open) MyConnection.Open();
        }
        #endregion

        #region Constructor Singleton
        static Connection instance = new Connection();
        public static Connection GetInstance() => instance;
        private Connection()
        {
            string ConnectionString = "";
            if (File.Exists(PathConfig))
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            } else if (File.Exists(PathConnection))
            {
                ConnectionString = File.ReadAllText(PathConnection);
            }
            else throw new Exception("Error: no existen datos de conexión.");
            MyConnection.ConnectionString = ConnectionString;
        }
        #endregion

        #region IParameters
        public void AddBit(string name, bool value)
        {
            MyCommand.Parameters.Add("@" + name, SqlDbType.Bit).Value = value;
        }

        public void AddDateTime(string name, DateTime value)
        {
            MyCommand.Parameters.Add("@" + name, SqlDbType.DateTime).Value = value;
        }

        public void AddFloat(string name, double value)
        {
            MyCommand.Parameters.Add("@" + name, SqlDbType.Float).Value = value;
        }

        public void AddInt(string name, int value)
        {
            MyCommand.Parameters.Add("@" + name, SqlDbType.Int).Value = value;
        }

        public void AddVarchar(string name, int length, string value)
        {
            MyCommand.Parameters.Add("@" + name, SqlDbType.VarChar, length).Value = value;
        }
        #endregion

        #region IConnection
        public void CreateCommand(string storedProc, string data)
        {
            MyCommand = new SqlCommand(storedProc, MyConnection);
            MyCommand.CommandType = CommandType.StoredProcedure;
            AddData = data;
        }

        public void Delete()
        {
            OpenConnection();
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception("No se pudo Eliminar " + AddData +": "+ e.Message);
            }
            finally
            {
                MyConnection.Close();
            }
        }

        public DataRow Find()
        {
            OpenConnection();
            try
            {
                DataTable dt = new DataTable();
                dt.Load(MyCommand.ExecuteReader());
                return dt.Rows[0];
            } catch(Exception e)
            {
                throw new Exception("No fue posible buscar " + AddData + ": " + e.Message);
            } finally { MyConnection.Close(); }
        }

        public int Insert()
        {
            OpenConnection();
            try
            {
                int i = int.Parse(MyCommand.ExecuteScalar().ToString());
                return i;
            } catch(Exception e)
            {
                throw new Exception("No se pudo insertar " + AddData + ": " + e.Message);
            } finally { MyConnection.Close(); }
        }

        public bool Exists()
        {
            OpenConnection();
            try
            {
                int i = int.Parse(MyCommand.ExecuteScalar().ToString());
                return i > 0;
            } catch (Exception e)
            {
                throw new Exception("No se pudo comprobar la existencia de " + AddData + ": " + e.Message);
            } finally { MyConnection.Close(); }
        }

        public DataTable List()
        {
            OpenConnection();
            try
            {
                DataTable dt = new DataTable();
                dt.Load(MyCommand.ExecuteReader());
                return dt;
            } catch(Exception e)
            {
                throw new Exception("Error al listar "+ AddData + ": " + e.Message);
            } finally
            {
                MyConnection.Close();
            }
        }

        public void Update() {
            OpenConnection();
            try { MyCommand.ExecuteNonQuery(); }
            catch(Exception e)
            {
                throw new Exception("No se pudo actualizar " + AddData + ": " + e.Message);
            } finally { MyConnection.Close(); }
        }
        #endregion
    }
}
