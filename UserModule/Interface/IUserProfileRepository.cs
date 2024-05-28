using GlobalUtility.Interface;
using UserModule.Database;
using UserModule.Model;

namespace UserModule.Interface
{
    public interface IUserProfileRepository : IRepositoryBase<UserProfileModel, UserDB>
    {
        Task<UserProfileModel?> FindByNameAsync(string name);
    }
}
