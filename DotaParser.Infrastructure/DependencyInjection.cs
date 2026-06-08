using DotaParser.Application.Common.Interfaces;
using DotaParser.Application.Players.EventHandlers;
using DotaParser.Infrastructure.Data;
using DotaParser.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotaParser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрируем типизированный HttpClient для OpenDotaService
        services.AddHttpClient<IOpenDotaService, OpenDotaService>(client =>
        {
            client.BaseAddress = new Uri("https://api.opendota.com/api/");
        })
        // Добавляем стандартные политики отказоустойчивости (Retry, Timeout, Circuit Breaker).
        // Если OpenDota ответит 429 Rate Limit, клиент автоматически подождет и повторит запрос.
        .AddStandardResilienceHandler();

        services.AddStackExchangeRedisCache(options =>
        {
            // Берем строку подключения локально. 
            // В реальном проекте её надо брать из IConfiguration
            options.Configuration = "localhost:6379";
            options.InstanceName = "DotaParser_";
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDotaConstantsProvider, DotaConstantsProvider>();

        services.AddHostedService<RedisConstantsSeederService>();

        // Настройка MassTransit и RabbitMQ
        services.AddMassTransit(x =>
        {
            // Регистрируем все Consumer'ы из сборки слоя Application автоматически
            x.AddConsumers(typeof(PlayerLoggedInEventConsumer).Assembly);

            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("dota-parser", false));

            x.UsingRabbitMq((context, cfg) =>
            {
                // Подключаемся к RabbitMQ из докера (localhost:5672)
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Автоматически создает очереди и привязывает их к нашим Consumer'ам
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}