using DotaParser.Application.Common.Caching;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotaParser.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // Регистрируем наш Behavior
            cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
        });
        return services;
    }
}