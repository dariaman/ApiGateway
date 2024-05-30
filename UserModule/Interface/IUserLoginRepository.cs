using GlobalUtility.Interface;
using UserModule.Database;
using UserModule.Model;

namespace UserModule.Interface
{
    public interface IUserLoginRepository : IRepositoryBase<UserLoginModel, UserDB>
    {
        Task<UserLoginModel?> FindByUsernamePasswordAsync(string username, string passwordHash);
        Task<UserLoginModel?> FindByUsernameAsync(string username);
        Task<UserLoginModel?> FindByUserProfileIDAsync(Int64 userID);
    }
}
