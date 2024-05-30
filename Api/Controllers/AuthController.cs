using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModule.Interface;
using UserModule.Request;

namespace Api.Controllers
{
    public class AuthController(IAuthService authService) : MainController
    {
        public readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginReq userLoginReq)
        {
            var token = await _authService.AuthenticateAsync(userLoginReq);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("test")]
        public async Task<IActionResult> test() => Ok();
    }
}
