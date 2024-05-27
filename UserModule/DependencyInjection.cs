using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Database;

namespace UserModule
{
    public static class DependencyInjection
    {
        public static IServiceCollection UserModuleDI(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<UserDB>(options => options.UseSqlServer(config.GetSection("Database:UserDB").Value));
            return services;
        }
    }
}
