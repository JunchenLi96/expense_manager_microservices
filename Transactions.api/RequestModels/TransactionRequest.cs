using System.ComponentModel.DataAnnotations;
using Transactions.api.DomainModels;

namespace Transactions.api.RequestModels
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