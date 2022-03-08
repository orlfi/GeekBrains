using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Interfaces;
using Restaurant.Booking;
using Restaurant.Booking.Services;
using Restaurant.Notifications.Sms;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddSingleton<Application>();
    services.AddSingleton<IRestaurant, Restaurant.Booking.Services.Restaurant>();
    services.AddSingleton<IProducer, SmsGateway>();
    services.AddSingleton<IOrderManager, InteractiveOrderManager>();
    services.AddHostedService<Application>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
 host.Run();
