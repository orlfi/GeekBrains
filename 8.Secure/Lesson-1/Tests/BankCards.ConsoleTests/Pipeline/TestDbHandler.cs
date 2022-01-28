using Orlfi.CommonLibrary.Pipeline.Base;
using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace BankCards.ConsoleTests.Pipeline;

public class TestDbHandler : AbstractPipelineHandler<TestResult>
{
    protected override bool Handle(IPipelineContext<TestResult> context)
    {
        throw new NotImplementedException();
    }
}
