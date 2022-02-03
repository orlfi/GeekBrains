using BankCards.ConsoleTests.Pipeline;
using Microsoft.Extensions.Logging;
using Orlfi.CommonLibrary.Pipeline.Base;

namespace BankCards.ConsoleTests;

public class Application
{
    //private static string _path = null!;
    //public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;
    private readonly TestRuntimeLibraryHandler _testRuntimeLibraryHandler;
    private readonly TestWebApiHandler _testWebApiHandler;
    private readonly TestDbHandler _testDbHandler;

    public Application(
        TestRuntimeLibraryHandler testRuntimeLibraryHandler,
        TestWebApiHandler testWebApiHandler,
        TestDbHandler testDbHandler,
        ILogger<Application> logger)
    {
        _logger = logger;
        _testRuntimeLibraryHandler = testRuntimeLibraryHandler;
        _testWebApiHandler = testWebApiHandler;
        _testDbHandler = testDbHandler;
    }

    public async Task RunAsync()
    {
        try
        {
            _logger.LogInformation("Application started");
            
            var context = new PipelineContext<TestResult>(new TestResult());
            var pipeline = PipelineBuilder<TestResult>.Create<TestResult>()
                .Use(_testRuntimeLibraryHandler)
                .Use(_testDbHandler)
                .Use(_testWebApiHandler)
                .Build();
            await pipeline.RunAsync(context);

            PrintInfo(context.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }

    private void PrintInfo(TestResult result)
    {
        Console.WriteLine("\r\nFrom DB:");
        Helpers.PrintCards(result.CardsFromDb); ;
        Helpers.PrintUsers(result.UsersFromDb);

        Console.WriteLine("\r\nFrom Api:");
        Helpers.PrintCards(result.CardsFromApi); ;
    }
}