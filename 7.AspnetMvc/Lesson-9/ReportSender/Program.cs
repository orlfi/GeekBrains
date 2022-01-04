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
using Scheduler;
using Scheduler.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureApp)
    .ConfigureServices(ConfigureServices)
    .UseSerilog(ConfigureLogger);

static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder builder)
{
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddTransient<MemoryDatabase>();
    services.AddTransient(typeof(IMemoryRepository<>), typeof(MemoryRepository<>));
    services.AddTransient<IReportManager, ReportManager>();
    services.AddScoped<Application>();
    services.AddScoped<IMailGatewayBuilder, MailGatewayBuilder>();
    services.AddScoped<IEmployeeReport, EmployeeHtmlReport>();
    services.AddScoped<ISchedulerFactory, StdSchedulerFactory>();
    services.AddScoped<IJobFactory, JobFactory>();
    services.AddSingleton<EmailSenderJob>();
    services.AddSingleton(new JobSchedule(typeof(EmailSenderJob), context.Configuration.GetValue<string>("CronExpression")));
    services.AddHostedService<ScheduleHostedService>();
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