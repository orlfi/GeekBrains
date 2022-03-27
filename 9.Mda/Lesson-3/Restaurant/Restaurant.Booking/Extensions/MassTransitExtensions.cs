﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Messaging.Configuration;

namespace Restaurant.Booking.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitSettings = configuration.GetSection("RabbitSettings").Get<RabbitSettings>();

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitSettings.Host, rabbitSettings.Port, "/", h =>
                {
                    h.Username(rabbitSettings.UserName);
                    h.Password(rabbitSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddMassTransitHostedService(true);
        return services;
    }
}