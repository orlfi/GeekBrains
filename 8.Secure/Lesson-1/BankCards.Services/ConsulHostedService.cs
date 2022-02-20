using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BankCards.Services;

public class ConsulHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IConsulClient _consulClient;
    private readonly ConsulSettings _consulSettings;
    private readonly HttpContext _httpContext;
    private CancellationTokenSource _cts;
    private string _serviceId;


    public ConsulHostedService(ILogger<ConsulHostedService> logger, IConsulClient consulClient, IHttpContextAccessor httpContextAccessor, ConsulSettings consulSettings)
    {
        _logger = logger;
        _consulClient = consulClient;
        _consulSettings = consulSettings;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting ConsulHostedService...");
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        var address = _consulSettings.ServiceAddress;
        var uri = new Uri(address);
        _serviceId = "Service-v1-" + Dns.GetHostName() + "-" + uri.Authority;

        var registration = new AgentServiceRegistration()
        {
            ID = _serviceId,
            Name = "BankService",
            Address = uri.Host,
            Port = uri.Port,
            Tags = new[] { "api" },
            Check = new AgentServiceCheck()
            {
                // Сердцебиение и адрес
                HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/{_consulSettings.HealthPath}",
                // сверхурочное время
                Timeout = TimeSpan.FromSeconds(2),
                // проверяем интервал
                Interval = TimeSpan.FromSeconds(10)
            }
        };
        // Сначала удалите сервис, чтобы избежать повторной регистрации
        await _consulClient.Agent.ServiceDeregister(registration.ID, _cts.Token);
        await _consulClient.Agent.ServiceRegister(registration, _cts.Token);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stoping ConsulHostedService...");
        _cts.Cancel();
        await _consulClient.Agent.ServiceDeregister(_serviceId, cancellationToken);
    }
}
