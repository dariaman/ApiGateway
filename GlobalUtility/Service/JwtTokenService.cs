using GlobalUtility.Configuration;
using GlobalUtility.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlobalUtility.Service
{
    public class JwtTokenService(IOptions<JwtSetting> jwtSetting) : IJwtTokenService
    {
        private readonly JwtSetting _jwtSetting = jwtSetting.Value;

        // return token (string)
        // with claim userID and user Fullname
        public string GenerateJwtToken(string userID, string userFullname)
        {
            var SecretKey = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256);

            var Claims = new List<Claim>
            {
                new("UserID", userID),
                new("Fullname", userFullname),
            };

            var SecurityToken = new JwtSecurityToken(
                    issuer: _jwtSetting.Issuer,
                    audience: _jwtSetting.Audience,
                    expires: DateTime.UtcNow.AddHours(_jwtSetting.ExpiryHours),
                    claims: Claims,
                    signingCredentials: SigningCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
        }

        // validate token 
        public int ValidateJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Invalid Token");

            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _jwtSetting.Issuer,
                ValidAudience = _jwtSetting.Audience,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            _ = int.TryParse(jwtToken.Claims.SingleOrDefault(x => x.Type == "UserID")?.Value, out int userID);

            if (userID > 0) return userID;
            else throw new ArgumentException("Invalid Token");

        }

        //public static RefreshToken GenerateRefreshToken(string ipAddress)
        //{
        //    var randomBytes = new byte[64];
        //    using var rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(randomBytes);
        //    return new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(randomBytes),
        //        Expires = DateTime.UtcNow.AddMinutes(2),
        //        Created = DateTime.UtcNow,
        //        CreatedByIp = ipAddress
        //    };
        //}
    }
}
