using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceString = "Пример текстовой строки";

            Console.WriteLine($"Исходная строка: {sourceString}");
            Console.WriteLine($"Перевернутая строка: {sourceString.Reverse()}");
            Console.ReadKey();
        }
    }
}
