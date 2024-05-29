using GlobalUtility.Entity;
using UserModule.Database;
using UserModule.Interface;
using UserModule.Model;

namespace UserModule.Repository
{
    public class UserLoginRepository(UserDB userDBContext) : RepositoryBase<UserLoginModel, UserDB>(userDBContext), IUserLoginRepository
    {

    }
}
