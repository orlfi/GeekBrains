using ScannerLibrary.Interfaces;

namespace ScannerLibrary.Loggers;

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
