using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScannerLibrary;
using ScannerLibrary.Devices;
using ScannerLibrary.Loggers;
using ScannerLibrary.Savers;
using TestApp;

using var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices(services =>
    {
        services.AddSingleton<Application>();
        // services.AddTransient<MyService>();
    })
    .Build();

host.Services.GetRequiredService<Application>().Run();
Console.ReadLine();