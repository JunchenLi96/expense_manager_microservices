using System.Threading.Tasks;
using ExpenseManagerUsers.DomainModels;

namespace ExpenseManagerUsers.Services
{
    public interface IUserService
    {

        Task AddNewUserAsync(AppUser user);
        Task<bool> CheckUserExists(string username);
        Task<AppUser> GetUser(string username);

    }
}