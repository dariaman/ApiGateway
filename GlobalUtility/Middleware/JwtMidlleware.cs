using Microsoft.AspNetCore.Http;

namespace GlobalUtility.Middleware
{
    public class JwtMidlleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("=============================== Middleware");
            Console.WriteLine("=============================== Middleware");

            await next(context);
        }
    }
}
