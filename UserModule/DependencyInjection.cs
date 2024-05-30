using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Database;
using UserModule.Interface;
using UserModule.Repository;
using UserModule.Service;

namespace UserModule
{
    public static class DependencyInjection
    {
        public static IServiceCollection UserModuleDI(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserDB>(options => options.UseSqlServer(config.GetSection("Database:UserDB").Value));

            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IUserLoginRepository, UserLoginRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}
