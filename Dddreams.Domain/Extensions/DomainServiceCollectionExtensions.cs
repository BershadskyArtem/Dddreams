using Dddreams.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Dddreams.Domain.Extensions;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;   
    }
}