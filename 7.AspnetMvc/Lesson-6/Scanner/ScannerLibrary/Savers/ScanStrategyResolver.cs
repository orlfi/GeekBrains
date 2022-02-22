using Autofac.Features.Indexed;
using Microsoft.Extensions.DependencyInjection;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Savers;

public class ScanStrategyResolver : IScanStrategyResolver
{
    private readonly IIndex<string, IScanSaver> _savers;

    public ScanStrategyResolver(IIndex<string, IScanSaver> savers)
    {
        _savers = savers;
    }

    public IScanSaver GetScanSaverByName(string name)
    {
        return _savers[name];
    }
}
