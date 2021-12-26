using System.Diagnostics;
using Hardwares.Domain;
using Hardwares.Interfaces;
using Hardwares.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hardwares.ConsoleTests;

public class Application
{
    private readonly IRepository<Hardware> _hardwareRepository;
    private readonly IDbInitalizer _dbInitializer;
    private readonly bool _removeDatabase;
    private readonly ILogger<Application> _logger;

    public Application(IRepository<Hardware> hardwareRepository, 
        IDbInitalizer dbInitializer, 
        IConfiguration config, 
        ILogger<Application> logger)
    {
        _hardwareRepository = hardwareRepository;
        _dbInitializer = dbInitializer;
        _removeDatabase = config.GetValue<bool>("RemoveDatabase");
        _logger = logger;
    }

    public async Task RunSync(CancellationToken cancel = default)
    {
        try
        {
            await _dbInitializer.Initialize(_removeDatabase, cancel).ConfigureAwait(false);
            await TestHardwareRepository(cancel).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Отмена операции");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанное исключение {0}", ex.Message);
        }
    }

    private async Task TestHardwareRepository(CancellationToken cancel = default)
    {
        _logger.LogInformation("Тестируем запрос всех записей оборудования из БД...");
        Stopwatch sw = Stopwatch.StartNew();
        var items = await _hardwareRepository.GetAllAsync(default).ConfigureAwait(false);
        Console.WriteLine("{0,2} {1,-20} {2,-30} {3,14} {4,-10}", "ID", "Наименование", "Характеристики", "Дата установки", "Стоимость");
        foreach (var item in items)
        {
            Console.WriteLine("{0,2} {1,-20} {2,-30} {3,10:dd.MM.yyyy} {4,10:N0} руб.", item.Id, item.Name, item.Description, item.InstallationDate, item.Cost);
        }
        sw.Stop();
        _logger.LogInformation("Получено {0} записей из таблицы оборудования за {1} мс", items.Count(), sw.ElapsedMilliseconds);
    }
}
