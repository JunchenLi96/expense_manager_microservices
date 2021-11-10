namespace ExpenseManagerUsers.DomainModels
{
    public class AppUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}