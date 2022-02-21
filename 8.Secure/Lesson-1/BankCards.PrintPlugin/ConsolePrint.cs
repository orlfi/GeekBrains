using System;

namespace BankCards.StrongNameLib
{
    public static class ConsolePrint
    {
        public static void ColorPrintLine(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
