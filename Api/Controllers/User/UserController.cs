﻿using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.User
{
    public class UserController(IUserService userService) : MainController
    {
        public readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterParamReq)
        {
            await _userService.Register(userRegisterParamReq);
            return Ok();
        }
    }
}
