using System.Diagnostics;
using Hardwares.DAL.Repositories;
using Hardwares.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleTests
{
    internal class Application
    {
        private readonly Repository<Hardware> _hardwareRepository;
        private readonly ILogger<Application> _logger;

        public Application(Repository<Hardware> hardwareRepository, ILogger<Application> logger)
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
            _logger.LogInformation("Получено {0} записей из таблицы оборудования за {1}", items.Count, sw.ElapsedMilliseconds.ToString());
        }
    }
}
