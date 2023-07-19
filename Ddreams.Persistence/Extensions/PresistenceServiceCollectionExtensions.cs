using Dddreams.Application.Interfaces.Repositories;
using Ddreams.Persistence.Contexts;
using Ddreams.Persistence.Interceptors;
using Ddreams.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ddreams.Persistence.Extensions;

public static class PresistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPresistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        
        
        
        services.AddDbContext<ApplicationDbContext>((isp, opt) =>
        {
            var interceptor = isp.GetService<UpdateAuditableEntitiesInterceptor>();
            if (interceptor == null)
                throw new NullReferenceException("Unable to create update interceptor.");
            
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(interceptor);
        });

        services.AddTransient<ICommentsRepository, CommentsRepository>();
        services.AddTransient<IDreamsRepository, DreamsRepository>();
        services.AddTransient<ILikesRepository, LikesRepository>();
        services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        
        
        return services;
    }
}