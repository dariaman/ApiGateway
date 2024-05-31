using GlobalUtility.Entity;
using UserModule.Model;
using UserModule.Request;

namespace UserModule.Interface
{
    public interface IUserService
    {
        Task<UserProfileModel> RegisterUserAsync(UserRegisterReq userRegisterParam);
        Task SetUserSessionAsync(Int64 userID);
    }
}
