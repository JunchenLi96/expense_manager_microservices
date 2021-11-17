using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ExpenseManagerDbContext;
using ExpenseManagerTransactions.DomainModels;
using ExpenseManagerTransactions.Services;
using Microsoft.Extensions.Logging;

namespace ExpenseManagerTransactions.Service
{
    public class TransactionService: ITransactionService
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;

        public TransactionService(DbContext dbContext, ILogger<TransactionService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Transaction> CreateNewTransaction(Transaction transaction)
        {

            //TransactionToAdd.Type = TransactionToAdd.TransactionType.ToString();
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"INSERT INTO Transactions (Description,CreatedAt,type,value, UserID) OUTPUT INSERTED.* VALUES (@Description, @CreatedAt, @Type, @value, @UserID)";

                var result = await dbConnection.QuerySingleAsync<Transaction>(query, transaction);

                return result;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(string userID)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"SELECT * FROM Transactions WHERE UserID=@userID";

                IEnumerable<Transaction> transactions = await dbConnection.QueryAsync<Transaction>(query, new { userID });

                return transactions;
            }
        }

        public async Task<Transaction> GetTransactionByID(string ID)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"SELECT * FROM Transactions WHERE ID=@ID";

                Transaction transactions = await dbConnection.QuerySingleOrDefaultAsync<Transaction>(query, new { ID });

                return transactions;
            }
        }

        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {

            //transaction.Type = transaction.TransactionType.ToString();
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = "UPDATE Transactions set Description=@Description ,UpdatedAt=@UpdatedAt ,type=@Type ,value=@value OUTPUT INSERTED.* WHERE ID=@ID AND UserID = @UserID";

                var result = await dbConnection.QuerySingleOrDefaultAsync<Transaction>(query, transaction);

                return result;
            }
        }

        public async Task<int> DeleteTransaction(int ID, string UserID)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = "Delete Transactions WHERE ID= @ID AND UserID = @UserID";

                int result = await dbConnection.ExecuteAsync(query, new { ID, UserID });
                _logger.LogInformation($"{result} rows deleted");
                return result;
            }
        }

    }
}