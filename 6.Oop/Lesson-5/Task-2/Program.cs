using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var complex1 = new Complex(3, 8);
            var complex2 = new Complex(2, 4);

            Console.WriteLine("+: {0}", complex1 + complex2);
            Console.WriteLine("-: {0}", complex1 - complex2);
            Console.WriteLine("*: {0}", complex1 * complex2);
            Console.WriteLine("/: {0}", complex1 / complex2);
            Console.WriteLine("=: {0}", complex1 == complex2);
            Console.WriteLine("!=: {0}", complex1 != complex2);
        }
    }
}
