using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagerTransactions.DomainModels;

namespace ExpenseManagerTransactions.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactions(string username);
        Task<Transaction> GetTransactionByID(string ID);
        Task<Transaction> CreateNewTransaction(Transaction transactionToCreate);
        Task<Transaction> UpdateTransaction(Transaction transactionToUpdate);
        Task<int> DeleteTransaction(int ID, string UserID);
    }
}