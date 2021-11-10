using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagerTransactions.DomainModels
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);
    }
}