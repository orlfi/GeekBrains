using System;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var parser = new EmailParser("text.txt");
            var rows = await parser.GetEmails();
            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }
        }
    }
}
