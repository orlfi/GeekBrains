using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScannerLibrary;
using ScannerLibrary.Devices;
using ScannerLibrary.Interfaces;
using ScannerLibrary.Savers;
using TestApp;

using var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb =>
    {
        // для шаблона Strategy
        cb.RegisterType<ScanToPdf>().Named<IScanSaver>("pdf").SingleInstance();
        cb.RegisterType<ScanToWord>().Named<IScanSaver>("word").SingleInstance();
    }))
    .ConfigureServices(services =>
    {
        services.AddSingleton<Application>();
        services.AddSingleton<IScannerContext, ScannerContext>();
        services.AddSingleton<IScanStrategyResolver, ScanStrategyResolver>();
        services.AddSingleton<IScannerDevice>(p =>
        {
            var config = p.GetRequiredService<IConfiguration>();
            return new ScannerDevice(config.GetRequiredSection("scanner_path").Value);
        });
        services.AddSingleton<ScanStrategyName>(p =>
        {
            var config = p.GetRequiredService<IConfiguration>();
            return new ScanStrategyName { Name = config.GetRequiredSection("scan_strategy").Value };
        });

    })
    .Build();

host.Services.GetRequiredService<Application>().Run();
Console.ReadLine();