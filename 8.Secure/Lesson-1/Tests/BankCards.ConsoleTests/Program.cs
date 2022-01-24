using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BankCards.ConsoleTests;
using BankCards.DAL.Context;
using BankCards.Interfaces.Data.Account;
using BankCards.Services.DTO;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.Configure<LoginRequest>(context.Configuration.GetSection("LoginOptions"));

    services.AddHttpClient<Application>(options =>
    {
        options.BaseAddress = new Uri(context.Configuration["ApiUrl"]);
    });

    services.AddDbContext<BankContext>(options =>
    {
        var useDbOption = context.Configuration["UseDb"]?.ToLower();

        switch (useDbOption)
        {
            case "postgres":
                options.UseNpgsql(context.Configuration.GetConnectionString("postgres"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                break;
            default:
                options.UseSqlServer(context.Configuration.GetConnectionString("default"));
                break;
        }
    });
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
await host.Services.GetRequiredService<Application>().RunAsync();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();