using MySql.Data.MySqlClient;
using System.Data;

namespace FraudDetection.Adapter.Database
{
    public class MySqlConnectionFactory : IDBConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
