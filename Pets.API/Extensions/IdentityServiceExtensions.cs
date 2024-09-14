using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pets.Application.Entities;
using Pets.Persistence.Data;
using System.Text;

namespace Pets.API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = true;
        })
         .AddRoles<AppRole>()
          .AddRoleManager<RoleManager<AppRole>>()
         .AddSignInManager<SignInManager<AppUser>>()
         .AddEntityFrameworkStores<PetsDbContext>();   
        
        var tokenKey = config["Token:Key"];
        if (string.IsNullOrEmpty(tokenKey))
        {
            throw new ArgumentNullException(nameof(config), "TokenKey cannot be null or empty.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}
