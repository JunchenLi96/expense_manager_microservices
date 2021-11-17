using System.Data;
using System.Threading.Tasks;
using Dapper;
using ExpenseManagerDbContext;
using ExpenseManagerUsers.DomainModels;
using Microsoft.Extensions.Logging;

namespace ExpenseManagerUsers.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;
        private readonly ILogger _logger;

        public UserService(DbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddNewUserAsync(AppUser user)
        {
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"INSERT INTO Users (UserName , PasswordHash) VALUES(@UserName, @PasswordHash)";
                int result = await dbConnection.ExecuteAsync(query, user);
                _logger.LogInformation($"{result} rows added");
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
    }
}