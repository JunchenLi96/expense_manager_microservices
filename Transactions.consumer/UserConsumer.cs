using System.Data;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;
using Users.events;

namespace Transactions.consumer
{
    public class UserConsumer : IConsumer<UserCreated>
    {
        private readonly DbContext _dbContext;
        ILogger<UserConsumer> _logger;

        public UserConsumer(DbContext dbContext, ILogger<UserConsumer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            _logger.LogInformation($"UserID: {context.Message.ID} User name: {context.Message.UserName}");
            UserCreated user = new UserCreated
            {
                ID = context.Message.ID,
                UserName = context.Message.UserName
            };
            using (IDbConnection dbConnection = _dbContext.Connection)
            {
                string query = @"INSERT INTO Users (ID , UserName)  VALUES(@ID, @UserName)";
                var result = await dbConnection.ExecuteAsync(query, user);
                _logger.LogInformation($"{result} line added");
            }
        }
    }
}