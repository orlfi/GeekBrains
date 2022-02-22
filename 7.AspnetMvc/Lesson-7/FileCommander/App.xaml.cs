using System;
using System.Windows;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FileCommander.Interfaces;
using FileCommander.Interfaces.Reports;
using FileCommander.Services;
using FileCommander.Services.Reports;
using FileCommander.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FileCommander;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? _host;

    public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IServiceProvider Services => Hosting.Services;

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureServices(ConfigureServices)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigureAutofacServices))
        .UseSerilog(ConfigureLogger);

    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IReportResolver, ReportResolver>();
        services.AddScoped<MainWindowViewModel>();
    }

    private static void ConfigureAutofacServices(ContainerBuilder containerBuilder)
    {
        // для шаблона Strategy
        containerBuilder.RegisterType<FileInfoReport>().Named<IReport>("file").SingleInstance();
        containerBuilder.RegisterType<DirectoryInfoReport>().Named<IReport>("directory").SingleInstance();
    }

    static void ConfigureLogger(HostBuilderContext context, LoggerConfiguration config)
    {
        config.ReadFrom.Configuration(context.Configuration);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var host = Hosting;
        await host.StartAsync().ConfigureAwait(true);

        base.OnStartup(e);
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        using var host = Hosting;
        await host.StopAsync().ConfigureAwait(true);
    }

}
