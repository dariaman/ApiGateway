using Api.Mapper;
using Api.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Database;
using UserModule.Interface;

namespace Api.Controllers.User
{
    public class UserController(IUserService userService) : MainController
    {
        //public readonly IUserService _userService = userService;
        public readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterReq userRegisterParamReq)
        {
            //await _userService.Register(userRegisterParamReq);
            var test = await _userService.RegisterUserAsync(userRegisterParamReq.MapToUserProfile());
            //return Ok(userRegisterParamReq);
            return Ok(test);
        }
    }
}
