using System;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var account1 = new BankAccount(570435.43m, AccountType.PaymentAccount);

            Console.WriteLine($"Счет: {account1.GetNumber()}");
            Console.WriteLine($"Тип: {account1.GetAccountType()}");
            Console.WriteLine($"Баланс: {account1.GetBalance():0.##}\r\n");

            var account2 = new BankAccount(AccountType.DepositAccount);

            Console.WriteLine($"Счет: {account2.GetNumber()}");
            Console.WriteLine($"Тип: {account2.GetAccountType()}");
            Console.WriteLine($"Баланс: {account2.GetBalance():0.##}\r\n");

            var account3 = new BankAccount(35.65m);

            Console.WriteLine($"Счет: {account3.GetNumber()}");
            Console.WriteLine($"Тип: {account3.GetAccountType()}");
            Console.WriteLine($"Баланс: {account3.GetBalance():0.##}");

            Console.ReadKey();
        }
    }
}
