using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FileCommander.Reports;
using FileCommander.Reports.Interfaces;
using FileCommander.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileCommander;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? _host;

    public static IHost Hosting => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureServices(ConfigureServices)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigureAutofacServices));

    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton<IReportResolver, ReportResolver>();
    }

    private static void ConfigureAutofacServices(ContainerBuilder containerBuilder)
    {
        // для шаблона Strategy
        containerBuilder.RegisterType<FileInfoReport>().Named<IReport>("file").SingleInstance();
        containerBuilder.RegisterType<DirectoryInfoReport>().Named<IReport>("directory").SingleInstance();
    }

}
