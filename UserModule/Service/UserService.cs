using GlobalUtility.Entity;
using System.Transactions;
using UserModule.Interface;
using UserModule.Mapper;
using UserModule.Model;
using UserModule.Request;

namespace UserModule.Service
{
    public class UserService(
        IUserProfileRepository userProfileRepository,
        IUserLoginRepository userLoginRepository,
        //UserSession userSession,
        IAuthService authService) : IUserService
    {

        public readonly IUserProfileRepository _userProfileRepository = userProfileRepository;
        public readonly IUserLoginRepository _userLoginRepository = userLoginRepository;
        public readonly IAuthService _authService = authService;
        //public UserSession _userSession = userSession;

        public async Task<UserProfileModel> RegisterUserAsync(UserRegisterReq userRegisterParam)
        {
            var olduser = await _userProfileRepository.FindByNameAsync(userRegisterParam.Fullname!);

            if (olduser is not null) throw new ArgumentException($"User Fullname {userRegisterParam.Fullname} already exist");

            UserProfileModel newUserProfile;
            UserLoginModel newUserLogin = new();

            using TransactionScope ts = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                newUserProfile = await _userProfileRepository.InsertAsync(userRegisterParam.MapToUserProfile());

                (newUserLogin.PasswordHash, newUserLogin.Salt) = _authService.GenerateNewHashPassword(userRegisterParam.Password!);
                newUserLogin.UserProfileId = newUserProfile.Id;
                newUserLogin.Username = userRegisterParam.Username!;

                await _userLoginRepository.InsertAsync(newUserLogin);

                ts.Complete();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            return newUserProfile;
        }

        public async Task<UserSession> GetUserSessionAsync(Int64 userID)
        {
            UserProfileModel userProfile = await _userProfileRepository.FindByIdAsync(userID) ?? throw new ApplicationException("can not create user session");
            UserLoginModel userLogin = await _userLoginRepository.FindByUserProfileIDAsync(userID) ?? throw new ApplicationException("can not create user session");

            return new UserSession
            {
                UserID = userProfile.Id,
                Username = userLogin.Username,
                Fullname = userProfile.Fullname,
                Email = userProfile.Email
            };
        }
    }
}
