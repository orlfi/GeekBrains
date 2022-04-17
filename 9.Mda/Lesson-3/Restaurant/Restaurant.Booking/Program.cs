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
using Restaurant.Booking.Models;
using Restaurant.Messaging.Interfaces;
using Restaurant.Messaging.Repositories;

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
    services.AddSingleton<IInMemoryRepository<BookingRequestModel>, InMemoryRepository<BookingRequestModel>>();
    services.AddSingleton<ITableBookingService, TableBookingService>();
    services.AddMessageBus(context.Configuration);

    services.AddHostedService<Worker>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

await CreateHostBuilder(args).Build().RunAsync();