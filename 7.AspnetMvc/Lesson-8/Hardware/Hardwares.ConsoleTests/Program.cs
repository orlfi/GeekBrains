using Autofac.Extensions.DependencyInjection;
using Hardwares.DAL.Context;
using Hardwares.DAL.Repositories;
using Hardwares.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hardwares.ConsoleTests;
public static class Program
{
    private static IHost __host;

    public static IHost Hosting => __host ??= CreateHostBuilder(Environment.GetCommandLineArgs())
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureAppConfiguration(app => app.AddJsonFile("appsettings.json"))
        .ConfigureServices(ConfigureServices)
        .Build();

    /// <summary>Для работы миграций</summary>
    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args);

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddDbContext<HardwaresDb>(opt => opt.UseSqlServer(context.Configuration.GetConnectionString("default")));
        services.AddSingleton<Application>();
        services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
    }

    public static async Task Main(string[] args)
    {
        await Hosting.Services.GetRequiredService<Application>().RunSync();
        Console.ReadLine();
    }
}
