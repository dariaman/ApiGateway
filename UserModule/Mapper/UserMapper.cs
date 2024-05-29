using UserModule.Model;
using UserModule.Request;

namespace UserModule.Mapper
{
    public static class UserMapper
    {
        public static UserProfileModel MapToUserProfile(this UserRegisterReq userRegisterReq)
        {
            return new UserProfileModel()
            {
                Email = userRegisterReq?.Email,
                Fullname = userRegisterReq?.Fullname
            };

        }
    }
}
