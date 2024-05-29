using GlobalUtility.Configuration;
using GlobalUtility.Interface;
using GlobalUtility.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalUtility
{
    public static class DependencyInjection
    {
        public static IServiceCollection GlobalUtilityDI(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
