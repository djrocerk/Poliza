﻿namespace Domain.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Authority { get; set; } = null!;
        public string Key { get; set; } = null!;
        public int AccessTokenExpirationMinutes { get; set; }
    }
}
