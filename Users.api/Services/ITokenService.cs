namespace Users.api.Services
{
    public interface ITokenService
    {
        string CreateToken(string username);
    }
}