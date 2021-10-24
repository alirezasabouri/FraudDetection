using System.Data;

namespace FraudDetection.Adapter.Database
{
    public interface IDBConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
