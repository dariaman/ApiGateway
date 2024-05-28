using UserModule.Model;

namespace UserModule.Interface
{
    public interface IUserService
    {
        Task<UserProfileModel> RegisterUserAsync(UserProfileModel userRegisterParam);
    }
}
