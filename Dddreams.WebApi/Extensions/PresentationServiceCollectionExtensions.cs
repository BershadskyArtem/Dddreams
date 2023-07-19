using Microsoft.OpenApi.Models;

namespace Dddreams.WebApi.Extensions;

public static class PresentationServiceCollectionExtensions
{
    public static IServiceCollection UseSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            var securityDefinition = new OpenApiSecurityScheme
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify your token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            };

            opt.AddSecurityDefinition("jwt_auth", securityDefinition);

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };

            var securityRequierments = new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            };

            opt.AddSecurityRequirement(securityRequierments);
        });
        return services;
    }
}