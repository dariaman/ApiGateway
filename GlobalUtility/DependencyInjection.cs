using GlobalUtility.Entity;
using GlobalUtility.Interface;
using GlobalUtility.Service;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalUtility
{
    public static class DependencyInjection
    {
        public static IServiceCollection GlobalUtilityDI(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddScoped<UserSession>();

            return services;
        }
    }
}
