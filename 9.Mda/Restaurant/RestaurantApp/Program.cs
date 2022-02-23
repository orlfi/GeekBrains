using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using RestaurantApp;
using Services;
using Services.Interfaces;
using Services.Gateways.Sms;
using Services.Request;

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
    services.AddSingleton<IRestaurant, Restaurant>();
    services.AddSingleton<IGateway, SmsGateway>();
    services.AddSingleton<IOrderManager, OrderManager>();
    services.AddSingleton<IOrderResultResolver, OrderResultResolver>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
await host.Services.GetRequiredService<Application>().RunAsync();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();