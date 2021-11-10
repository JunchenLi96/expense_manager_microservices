namespace ExpenseManagerTransactions.DomainModels
{
    public class Transaction
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public decimal Value { get; set; }
        public string UserID { get; set; }
        public TransactionType Type { get; set; }
    }
}