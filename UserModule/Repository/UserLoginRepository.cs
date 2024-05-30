using GlobalUtility.Entity;
using Microsoft.EntityFrameworkCore;
using UserModule.Database;
using UserModule.Interface;
using UserModule.Model;

namespace UserModule.Repository
{
    public class UserLoginRepository(UserDB userDBContext, UserSession userSession) 
        : RepositoryBase<UserLoginModel, UserDB>(userDBContext, userSession), IUserLoginRepository
    {
        public async Task<UserLoginModel?> FindByUsernamePasswordAsync(string username, string passwordHash)
        {
            return await userDBContext.UserLoginModel
                .SingleOrDefaultAsync(x => x.Username.Equals(username) && x.PasswordHash.Equals(passwordHash) && !x.IsDelete && x.IsActive);
        }

        public async Task<UserLoginModel?> FindByUsernameAsync(string username)
        {
            return await userDBContext.UserLoginModel
                .SingleOrDefaultAsync(x => x.Username.Equals(username) && !x.IsDelete && x.IsActive);
        }

        public async Task<UserLoginModel?> FindByUserProfileIDAsync(Int64 userID)
        {
            return await userDBContext.UserLoginModel.SingleOrDefaultAsync(x => x.UserProfileId.Equals(userID) && !x.IsDelete && x.IsActive);
        }
    }
}
