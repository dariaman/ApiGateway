using Api.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Database;

namespace Api.Controllers.User
{
    public class UserController(UserDB userDB) : MainController
    {
        //public readonly IUserService _userService = userService;
        public readonly UserDB _userDB = userDB;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterReq userRegisterParamReq)
        {
            //await _userService.Register(userRegisterParamReq);
            var test = _userDB.UserProfileModel.ToList();
            //return Ok(userRegisterParamReq);
            return Ok(test);
        }
    }
}
