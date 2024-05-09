using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Services;
using Services.Interfaces;

namespace API.Extensions
{
    public static class IoCExtension
    {
        public static void ResolveIoC(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();

        }

    }
}
