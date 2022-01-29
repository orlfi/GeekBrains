using BankCards.ConsoleTests.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orlfi.CommonLibrary.Pipeline.Base;
using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace BankCards.ConsoleTests.Pipeline;

public class TestRuntimeLibraryHandler : AbstractPipelineHandler<TestResult>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TestRuntimeLibraryHandler> _logger;

    public TestRuntimeLibraryHandler(IServiceProvider serviceProvider, ILogger<TestRuntimeLibraryHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override bool Handle(IPipelineContext<TestResult> context)
    {
        // создаю scope для вызова Dispose RuntimePrintManager
        using (var scope = _serviceProvider.CreateAsyncScope())
        {
            var _manager = scope.ServiceProvider.GetRequiredService<RuntimePrintManager>();
            _manager.Print("Тестовая строка", ConsoleColor.Red, ConsoleColor.Yellow);
        }
        return true;
    }

    protected override Task<bool> HandleAsync(IPipelineContext<TestResult> context, CancellationToken cancel = default)
    {

        return Task.FromResult(Handle(context));
    }
}
