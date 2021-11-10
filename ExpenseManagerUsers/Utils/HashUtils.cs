using System;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseManagerUsers.Utils
{
    public class HashUtils
    {
        public static string ComputeHash(string password)
        {
            using SHA256 sha = SHA256.Create();

            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
    }
}