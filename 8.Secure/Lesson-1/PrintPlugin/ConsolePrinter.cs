using System;

namespace PrintPlugin;

public class ConsolePrinter
{
    public void PrintColorLine(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }
}
