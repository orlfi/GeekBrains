using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Restaurant.Notifications;
using Restaurant.Messaging.Configuration;
using Microsoft.Extensions.Options;
using Restaurant.Messaging.Mq;
using Restaurant.Messaging.Interfaces;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.Configure<RabbitSettings>(context.Configuration.GetSection("RabbitSettings"));
    services.AddSingleton(p => p.GetRequiredService<IOptions<RabbitSettings>>().Value);
    services.AddSingleton<Background>();
    services.AddSingleton<IConsumer, RabbitConsumer>();
    services.AddHostedService<Background>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
host.Run();