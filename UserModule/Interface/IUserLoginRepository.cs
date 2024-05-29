using GlobalUtility.Interface;
using UserModule.Database;
using UserModule.Model;

namespace UserModule.Interface
{
    public interface IUserLoginRepository : IRepositoryBase<UserLoginModel, UserDB>
    {
    }
}
