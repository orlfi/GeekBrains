using System.Reflection;
using System.Runtime.Loader;

namespace BankCards.ConsoleTests.Runtime;

public class PluginAssemblyLoadContext : AssemblyLoadContext
{
    public PluginAssemblyLoadContext() : base(isCollectible: true) { }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        return null;
    }
}
