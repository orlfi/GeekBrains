using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ScannerLibrary.Interfaces;

namespace TestApp;

public class Application
{
    private readonly IScannerContext _context;
    private readonly ILogger<Application> _logger;

    public Application(IScannerContext context, ILogger<Application> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Run()
    {
        try
        {
            _logger.LogInformation("Начало сканирования...");
            _context.Run("result.pdf");
            _logger.LogInformation("Закончено сканирование.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка сканирования");
        }
    }
}
