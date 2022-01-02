using MailService.DAL;
using MailService.DAL.Repositories;
using MailService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MailService;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;
    private readonly IMemoryRepository<Employee> _employees;

    public Application(ILogger<Application> logger, IMemoryRepository<Employee> employees)
    {
        _logger = logger;
        _employees = employees;
    }

    public async Task RunAsync()
    {
        try
        {
            await Task.Run(() => _logger.LogInformation("Application started"));
            foreach (var employee in _employees.GetAll())
            {
                Console.WriteLine("{0} {1}", employee.Name, employee.Email);
                foreach (var order in employee.Orders)
                {
                    Console.WriteLine("\t{0,-20} {1,10} {2,10} {3,10}", order.Product.Name, order.Product.Price, order.Count, order.Total);
                }
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }
}