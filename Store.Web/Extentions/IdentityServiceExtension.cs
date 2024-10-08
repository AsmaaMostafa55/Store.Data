﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntities;

namespace Store.Web.Extentions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            {
                var builder = services.AddIdentityCore<AppUser>();
                builder=new IdentityBuilder(builder.UserType,builder.Services);
                builder.AddEntityFrameworkStores<StoreIdentityDbContext>();
                builder.AddSignInManager<SignInManager<AppUser>>();
                services.AddAuthentication();
                return services;

            }
        }
    }
}
