using BankCards.ConsoleTests.Options;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BankCards.ConsoleTests.Runtime;

public class RuntimePrintManager : IDisposable
{
    private readonly string _pluginPath;
    private readonly PluginAssemblyLoadContext _context;

    private Assembly? _assembly = default;
    private Assembly assembly => _assembly ??= LoadAssembly();

    public RuntimePrintManager(PluginAssemblyLoadContext context, IOptions<PluginOptions> pluginOptions)
    {
        _pluginPath = pluginOptions.Value.Path;
        _context = context;
    }

    public void Print(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        var type = assembly.GetType("PrintPlugin.ConsolePrinter");
        var printMethod = type.GetMethod("PrintColorLine");
        var instance = Activator.CreateInstance(type);
        printMethod.Invoke(instance, new object[] { text, foregroundColor, backgroundColor });
    }


    public void Dispose()
    {
        Console.WriteLine($"Выгружаем контекст...");
        _context.Unload();
        _assembly = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        PrintAssemblies();
        Console.WriteLine($"Контекст выгружен");
    }

    private Assembly LoadAssembly()
    {
        var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), _pluginPath);
        Assembly assembly = _context.LoadFromAssemblyPath(assemblyPath);
        var publicKeyToken = assembly.GetName().GetPublicKeyToken();
        Console.Write("Имя сборки {0} {1}", 
            assembly.FullName,
            publicKeyToken is not null && publicKeyToken.Length > 0 
                ? $"Сборка подписана: публичный ключ: {Convert.ToHexString(publicKeyToken)}"
                : "Сборка не подписана"
        );

        PrintAssemblies();
        
        return assembly;
    }

    private void PrintAssemblies()
    {
        int counter = 0;
        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            Console.WriteLine($"{counter++,3} {asm.GetName().Name}");
    }
}
