using System.ComponentModel.DataAnnotations;
using ExpenseManagerTransactions.DomainModels;

namespace ExpenseManagerTransactions.RequestModels
{
    public class TransactionRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public TransactionType Type { get; set; }
    }
}