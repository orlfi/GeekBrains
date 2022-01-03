using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MailService;
using ReportSender.Interfaces.Reports;
using ReportSender.Interfaces;
using ReportSender.Services;
using ReportSender.Interfaces.Gateways;
using ReportSender.Services.Gateways.Mail;
using ReportSender.DAL;
using ReportSender.Interfaces.Repositories;
using ReportSender.DAL.Repositories;
using ReportSender.Services.Reports;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddScoped<Application>();
    services.AddScoped<IReportManager, ReportManager>();
    services.AddScoped<IMailGatewayBuilder, MailGatewayBuilder>();
    services.AddScoped<MemoryDatabase>();
    services.AddScoped(typeof(IMemoryRepository<>), typeof(MemoryRepository<>));
    services.AddScoped<IEmployeeReport, EmployeeHtmlReport>();
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
await host.StartAsync();
await host.Services.GetRequiredService<Application>().RunAsync();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
await host.StopAsync();