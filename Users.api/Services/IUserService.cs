using System.Threading.Tasks;
using Users.api.DomainModels;
using Users.events;

namespace Users.api.Services
{
    public interface IUserService
    {

        Task AddNewUserAsync(AppUser user);
        Task<bool> CheckUserExists(string username);
        Task<AppUser> GetUser(string username);

        Task sendToSB(UserCreated user);

    }
}