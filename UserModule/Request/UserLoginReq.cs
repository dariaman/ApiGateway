﻿namespace UserModule.Request
{
    public record UserLoginReq
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
