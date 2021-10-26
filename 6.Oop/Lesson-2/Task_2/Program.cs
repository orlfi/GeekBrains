using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var account1 = new BankAccount();
            account1.SetNumber();
            account1.SetBalance(570435.43m);
            account1.SetType(AccountType.PaymentAccount);

            Console.WriteLine($"Счет: {account1.GetNumber()}");
            Console.WriteLine($"Тип: {account1.GetAccountType()}");
            Console.WriteLine($"Баланс: {account1.GetBalance():0.##}\r\n");

            var account2 = new BankAccount();
            account2.SetNumber();
            account2.SetBalance(234.3m);
            account2.SetType(AccountType.DepositAccount);

            Console.WriteLine($"Счет: {account2.GetNumber()}");
            Console.WriteLine($"Тип: {account2.GetAccountType()}");
            Console.WriteLine($"Баланс: {account2.GetBalance():0.##}");
            Console.ReadKey();
        }
    }
}
