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

    public Task Run()
    {
        try
        {
            var context = ScannerContextBuilder.Create(new ScannerDevice("scanner.txt"))
                .WithLogger(new Logger())
                .WithSaver(new ScanToPdf())
                .Build();

            _context.Run("result.pdf");
            _context.ConfigureProcessor(new ScanToWord());
            _context.Run("result.docx");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка сканирования");
        }
    }
}
