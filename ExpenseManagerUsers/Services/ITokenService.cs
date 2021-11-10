namespace ExpenseManagerUsers.Services
{
    public interface ITokenService
    {
        string CreateToken(string username);
    }
}