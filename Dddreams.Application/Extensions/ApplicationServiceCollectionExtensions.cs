using Dddreams.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dddreams.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(AssemblyMarker.Assembly);
            opt.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(AssemblyMarker.Assembly, includeInternalTypes: true);
        
        return services;
    }
}