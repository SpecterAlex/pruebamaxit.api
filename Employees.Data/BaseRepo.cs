using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Employees.Data
{
    /// <summary>
    /// Class to manage connection strings
    /// Use for multiple providers from database
    /// </summary>
    public class BaseRepo
    {
        readonly IConfiguration _config;
        public BaseRepo(IConfiguration config)
        {
            this._config = config;
        }

        /// <summary>
        /// Obtiene conexion abierta a SqlServer
        /// </summary>
        /// <returns>Regresa conexion abierta a base de datos SqlServer</returns>
        public SqlConnection GetOpenConnection()
        {
            string connectionString = _config["ConnectionStrings:SqlServer"];
            SqlConnection connection = new(connectionString);
            connection.Open();
            return connection;
        }
    }
}
