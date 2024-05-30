using UserModule.Request;

namespace UserModule.Interface
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserLoginReq userLoginReq);

        ///<summary>
        ///return password Hash and password Salt (base64)
        ///</summary>
        (string, string) GenerateNewHashPassword(string passwordText);
        string GenerateNewHashPassword(string passwordText, string base64PasswordSalt);
        bool VerifiedHashPassword(string passwordText, string base64PasswordSalt, string passwordHash);
    }
}
