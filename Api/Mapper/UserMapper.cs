using Api.Request;
using UserModule.Model;

namespace Api.Mapper
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
