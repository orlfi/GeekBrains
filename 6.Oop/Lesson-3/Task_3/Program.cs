using System;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var parser = new EmailParser("text.txt");
            await foreach (var row in parser.EnumEmailsAsync())
            {
                Console.WriteLine(row);
            }
        }
    }
}
