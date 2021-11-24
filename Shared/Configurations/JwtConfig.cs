using System;

namespace Shared.Configurations
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public TimeSpan ExpiresIn { get; set; }
    }
}