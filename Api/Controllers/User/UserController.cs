using GlobalUtility.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Interface;
using UserModule.Request;

namespace Api.Controllers.User
{
    public class UserController(IUserService userService, UserSession userSession) : MainController
    {
        public readonly IUserService _userService = userService;
        public UserSession _userSession = userSession;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterReq userRegisterParamReq)
        {

            //await _userService.Register(userRegisterParamReq);
            var test = await _userService.RegisterUserAsync(userRegisterParamReq);
            //return Ok(userRegisterParamReq);

            return Ok(test);
        }
    }
}
