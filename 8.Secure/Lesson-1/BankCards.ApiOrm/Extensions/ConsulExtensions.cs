using Microsoft.Extensions.Options;
using BankCards.Services;
using Consul;

namespace BankCards.ApiOrm.Extensions;

public static class ConsulExtensions
{
    public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ConsulSettings>(config.GetSection("ConsulSettings"));
        services.AddSingleton<ConsulSettings>(services => services.GetRequiredService<IOptions<ConsulSettings>>().Value);

        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var consulSettings = p.GetRequiredService<ConsulSettings>();
            consulConfig.Address = new Uri(consulSettings.Address);
        }));
        services.AddHostedService<ConsulHostedService>();
        services.AddHealthChecks();

        return services;
    }
}
