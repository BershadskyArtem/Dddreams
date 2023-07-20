using System.Text;
using Dddreams.Application.Common.Auth;
using Dddreams.Domain.Entities;
using Dddreams.Infrastructure.Auth;
using Ddreams.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dddreams.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = new AuthConfiguration()
        {
            Key = configuration["JwtKey"] ?? "wowsuchempty@test.com",
            RefreshTokenLifeTimeInDays = 90,
            TokenLifeTimeInSeconds = 500,
            SuperAdminEmail = "somebadpractive@test.com"
        };

        services.AddSingleton(authConfig);
        
        services.AddTransient<IUserAuthService, UserAuthService>();
        services.AddDefaultIdentity<DreamsAccount>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        var key = configuration["JwtKey"];
        if (key is null)
            throw new NullReferenceException("Unable to resolve jwt key.");
        
        var jwtSettings = new JwtSettings()
        {
            Key = key
        };
        
        var tokenFullValidationParams = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };
        
        var tokenOnRefreshValidationParams = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = false
        };

        var tokenStore = new TokenValidationParametersStore(tokenFullValidationParams, tokenOnRefreshValidationParams);

        services.AddSingleton(tokenStore);
        
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = tokenFullValidationParams;
            opt.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    Console.WriteLine(c.Exception.Message);
                    return Task.CompletedTask;
                },
                OnForbidden = c =>
                {
                    var i = 10;
                    var e = c;
                    return Task.CompletedTask;
                }
            };
        });


        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("CanPostOrDelete", opt =>
            {
                opt.RequireClaim(Claims.Dreams.CanPostADream, "true");
            });
            
            opt.AddPolicy("CanLikeOrDislike", opt =>
            {
                opt.RequireClaim(Claims.Likes.CanLeaveALike, "true");
            });

        });
        
        return services;
    }
}