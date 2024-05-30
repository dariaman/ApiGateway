using GlobalUtility.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using UserModule.Interface;
using UserModule.Request;

namespace UserModule.Service
{
    public class AuthService(IUserLoginRepository userLoginRepository, IUserProfileRepository userProfileRepository, IJwtTokenService jwtTokenService) : IAuthService
    {
        public readonly IUserLoginRepository _userLoginRepository = userLoginRepository;
        public readonly IUserProfileRepository _userProfileRepository = userProfileRepository;
        public readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<string> AuthenticateAsync(UserLoginReq userLoginReq)
        {
            var usernameLogin = await _userLoginRepository.FindByUsernameAsync(userLoginReq.Username) ?? throw new ArgumentException("Invalid username");

            if (!VerifiedHashPassword(userLoginReq.Password, usernameLogin.Salt, usernameLogin.PasswordHash)) throw new ArgumentException("Invalid password");

            var userProfile = await _userProfileRepository.FindByIdAsync(usernameLogin.UserProfileId) ?? throw new ArgumentException("User profile not found");

            var tokenJwt = _jwtTokenService.GenerateJwtToken(userProfile.Id.ToString(), userProfile.Fullname);

            return tokenJwt;
        }

        // return password Hash and password salt base64
        public (string, string) GenerateNewHashPassword(string passwordText)
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);

            var passwordHash = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: passwordText,
                        salt: passwordSalt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));

            return (passwordHash, Convert.ToBase64String(passwordSalt));
        }

        // return password hash
        public string GenerateNewHashPassword(string passwordText, string base64PasswordSalt)
        {
            byte[] passwordSalt = Convert.FromBase64String(base64PasswordSalt);

            var passwordHash = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: passwordText,
                        salt: passwordSalt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));

            return passwordHash;
        }

        // check passwordText is PasswordHash
        public bool VerifiedHashPassword(string passwordText, string base64PasswordSalt, string passwordHash)
        {
            var newPasswordHash = GenerateNewHashPassword(passwordText, base64PasswordSalt);

            return (newPasswordHash == passwordHash);
        }
    }
}