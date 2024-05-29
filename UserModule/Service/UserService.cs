using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Transactions;
using UserModule.Interface;
using UserModule.Mapper;
using UserModule.Model;
using UserModule.Request;

namespace UserModule.Service
{
    public class UserService(IUserProfileRepository userProfileRepository, IUserLoginRepository userLoginRepository) : IUserService
    {

        public readonly IUserProfileRepository _userProfileRepository = userProfileRepository;
        public readonly IUserLoginRepository _userLoginRepository = userLoginRepository;

        public async Task<UserProfileModel> RegisterUserAsync(UserRegisterReq userRegisterParam)
        {
            var olduser = await _userProfileRepository.FindByNameAsync(userRegisterParam.Fullname!);

            if (olduser is not null) throw new ArgumentException($"User Fullname {userRegisterParam.Fullname} already exist");

            UserProfileModel newUserProfile;
            UserLoginModel newUserLogin = new();

            using TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                newUserProfile = await _userProfileRepository.InsertAsync(userRegisterParam.MapToUserProfile());

                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                newUserLogin.PasswordHash = HashPassword(userRegisterParam.Password!, salt);
                newUserLogin.Salt = Convert.ToBase64String(salt);
                // byte[] salt = Convert.FromBase64String(string data)
                newUserLogin.UserProfileId = newUserProfile.Id;
                newUserLogin.Username = userRegisterParam.Username!;

                await _userLoginRepository.InsertAsync(newUserLogin);

                ts.Complete();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            return newUserProfile;
        }

        public static string HashPassword(string passwordText, byte[] passwordSaltByte)
        {
            return Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: passwordText,
                        salt: passwordSaltByte,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8));
        }
    }
}
