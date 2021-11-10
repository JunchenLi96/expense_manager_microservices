using System;

namespace ExpenseManagerUsers.Configurations
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public TimeSpan ExpiresIn { get; set; }
    }
}

