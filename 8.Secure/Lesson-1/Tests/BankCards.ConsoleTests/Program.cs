﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BankCards.ConsoleTests;
using BankCards.DAL.Context;

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
    //services.AddDbContext<BankContext>(options =>
    //    options.UseSqlServer(context.Configuration.GetConnectionString("default")));
    services.AddDbContext<BankContext>(options =>
        options.UseNpgsql(context.Configuration.GetConnectionString("postgres")));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
{
    config.ReadFrom.Configuration(context.Configuration);
}

using IHost host = CreateHostBuilder(args).Build();
await host.Services.GetRequiredService<Application>().RunAsync();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();