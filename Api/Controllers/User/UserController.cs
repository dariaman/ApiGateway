using Api.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.User
{
    public class UserController() : MainController
    {
        //public readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterReq userRegisterParamReq)
        {
            //await _userService.Register(userRegisterParamReq);
            return Ok(userRegisterParamReq);
        }
    }
}
