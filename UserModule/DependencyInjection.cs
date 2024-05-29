﻿using Microsoft.EntityFrameworkCore;
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

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
