using System.Security.Claims;

namespace ExpenseManagerTransactions.Utils
{
    public class ClaimsExtensions
    {
        public static string GetUserID(ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}