using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount();
            account.SetNumber(23423);
            account.SetBalance(570435.43m);
            account.SetAccountType(AccountType.PaymentAccount);

            Console.WriteLine($"Счет: {account.GetNumber()}");
            Console.WriteLine($"Тип: {account.GetAccountType()}");
            Console.WriteLine($"Баланс: {account.GetBalance():0.##}");
            Console.ReadKey();
        }
    }
}
