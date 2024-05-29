﻿namespace GlobalUtility.Configuration
{
    public record JwtSetting
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpiryHours { get; set; } = 1;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
