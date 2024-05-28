using GlobalUtility.Entity;
using Microsoft.EntityFrameworkCore;
using UserModule.Database;
using UserModule.Interface;
using UserModule.Model;

namespace UserModule.Repository
{
    public class UserProfileRepository(UserDB userDBContext) : RepositoryBase<UserProfileModel, UserDB>(userDBContext), IUserProfileRepository
    {
        //private readonly UserDB _userDBContext = userDBContext;
        public async Task<UserProfileModel?> FindByNameAsync(string name)
        {
            return await userDBContext.UserProfileModel.FirstOrDefaultAsync(x => x.Fullname.Equals(name) && !x.IsDelete);
        }
    }
}
