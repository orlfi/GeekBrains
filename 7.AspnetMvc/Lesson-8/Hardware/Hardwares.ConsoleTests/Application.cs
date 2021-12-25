using System.Diagnostics;
using Hardwares.Domain;
using Hardwares.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Hardwares.ConsoleTests;

public class Application
{
    private readonly IRepository<Hardware> _hardwareRepository;

    private readonly ILogger<Application> _logger;

    public Application(IRepository<Hardware> hardwareRepository, ILogger<Application> logger)
    {
        _hardwareRepository = hardwareRepository;
        _logger = logger;
    }

    public async Task RunSync()
    {
        await TestHardwareRepository();
    }

    private async Task TestHardwareRepository()
    {
        _logger.LogInformation("Тестируем запрос всех записей оборудования из БД...");
        Stopwatch sw = Stopwatch.StartNew();
        var items = await _hardwareRepository.GetAllAsync();
        foreach (var item in items)
        {
            Console.WriteLine(item.Name);
        }
        sw.Stop();
        _logger.LogInformation("Получено {0} записей из таблицы оборудования за {1} мс", items.Count(), sw.ElapsedMilliseconds);
    }
}
