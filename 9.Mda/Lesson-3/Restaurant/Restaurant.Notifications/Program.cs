using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Restaurant.Notifications.Interfaces;
using Restaurant.Notifications.Extensions;
using Restaurant.Notifications.Services;
using MassTransit.Audit;
using Restaurant.Messaging.Logging;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddSingleton<INotifier, Notifier>();
    services.AddSingleton<IMessageAuditStore, AuditStore>();
    services.AddMessageBus(context.Configuration);
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

await CreateHostBuilder(args).Build().RunAsync();