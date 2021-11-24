using System.Security.Claims;

namespace Transactions.api.Utils
{
    public class ClaimsExtensions
    {
        public static string GetUserID(ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}