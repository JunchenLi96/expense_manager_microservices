using System.ComponentModel.DataAnnotations;

namespace ExpenseManagerUsers.RequestModels
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}