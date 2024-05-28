using UserModule.Interface;
using UserModule.Model;

namespace UserModule.Service
{
    public class UserService(IUserProfileRepository userProfileRepository) : IUserService
    {

        public readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

        public async Task<UserProfileModel> RegisterUserAsync(UserProfileModel userRegisterParam)
        {

            var userold = await _userProfileRepository.FindByNameAsync(userRegisterParam.Fullname);

            if (userold is not null) throw new ArgumentException($"User Fullname {userRegisterParam.Fullname} already exist");

            var newUser = await _userProfileRepository.AddAsync(userRegisterParam);
            return newUser;
        }
    }
}
