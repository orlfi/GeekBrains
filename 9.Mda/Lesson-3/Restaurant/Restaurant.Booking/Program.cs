using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Serilog;
using Restaurant.Booking;
using Restaurant.Booking.Services;
using Restaurant.Messaging.Configuration;
using Restaurant.Booking.Extensions;
using Restaurant.Booking.Interfaces;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddSingleton(p => p.GetRequiredService<IOptions<RabbitSettings>>().Value);
    services.AddSingleton<Worker>();
    services.AddSingleton<ITableBookingService, TableBookingService>();
    services.AddMessageBus(context.Configuration);
    services.AddHostedService<Worker>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

CreateHostBuilder(args).Build().Run();