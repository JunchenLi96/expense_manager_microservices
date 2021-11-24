using System.Data;
using System.Threading.Tasks;
using Dapper;
using Shared;
using Users.api.DomainModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Users.events;

namespace Users.api.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;

        private readonly IBus _bus;

        public UserService(DbContext dbContext, ILogger<UserService> logger, IBus bus)
        {
            _dbContext = dbContext;
            _logger = logger;
            _bus = bus;
        }

        public async Task AddNewUserAsync(AppUser user)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"INSERT INTO Users (UserName , PasswordHash) OUTPUT INSERTED.ID VALUES(@UserName, @PasswordHash)";
                string userID = await dbConnection.QuerySingleOrDefaultAsync<string>(query, user);
                _logger.LogInformation($"The UserID for added user is: {userID}, sending to Service bus");
                UserCreated userCreated = new UserCreated
                {
                    ID = userID,
                    UserName = user.UserName
                };
                await sendToSB(userCreated);
            }
        }

        public async Task<bool> CheckUserExists(string username)
        {
            AppUser user = await GetUser(username);
            return user != null;
        }

        public async Task<AppUser> GetUser(string username)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"SELECT * FROM Users WHERE UserName=@username";
                AppUser user = await dbConnection.QuerySingleOrDefaultAsync<AppUser>(query, new { username });
                return user;
            }
        }
        public async Task sendToSB(UserCreated user)
        {
            await _bus.Publish(
                user
            );
        }
    }
}