using GlobalUtility.Entity;
using GlobalUtility.Interface;
using UserModule.Interface;

namespace Api.Middleware
{
    public class JwtMiddleware(IJwtTokenService jwtTokenService, IUserService userService) : IMiddleware
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IUserService _userService = userService;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("==================================== JwtMiddleware");

            string? token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (token is not null)
            {
                int userId = _jwtTokenService.ValidateJwtToken(token);
                if (userId > 0)
                {
                    var user = await _userService.GetUserSessionAsync(userId);

                    ////on successful jwt validation, set user session globally
                    //var scope = app.ApplicationServices.CreateScope();
                    //var _usersession = scope.ServiceProvider.GetService<UserSession>();

                    //_usersession.UserID = user.UserID;
                    //_usersession.Fullname = user.Fullname;
                    //_usersession.Email = user.Email;
                }
            }

            await next(context);
        }
    }
}
