using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var val1 = new Rational(12, 5);
            var val2 = new Rational(3, 2);
            Console.WriteLine("Оператор *: {0}", val1 * val2);
            Console.WriteLine("Оператор /: {0}", val1 / val2);
            Console.WriteLine("Оператор %: {0}", val1 % val2);
            Console.WriteLine("Оператор >: {0}", val1 > val2);
            Console.WriteLine("Оператор <: {0}", val1 < val2);
            Console.WriteLine("Оператор >=: {0}", val1 >= val2);
            Console.WriteLine("Оператор <=: {0}", val1 <= val2);
            Console.WriteLine("Оператор ==: {0}", val1 == val2);
            Console.WriteLine("Оператор !=: {0}", val1 != val2);
            Console.WriteLine("++: {0}", ++val1);
            Console.WriteLine("--: {0}", --val1);

            Console.WriteLine("Double: {0}", (double)val1);
            Console.WriteLine("Float: {0}", (float)val1);
            Console.WriteLine("Int: {0}", (int)val1);

            Console.WriteLine("Equals: {0}", val1.Equals(val2));
            Console.WriteLine("Equals: {0}", val1.Equals(new Rational(12, 5)));
            Console.WriteLine("Hash: {0}", val1.GetHashCode());

        }
    }
}
