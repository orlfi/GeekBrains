using System;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var account1 = new BankAccount(570435.43m, AccountType.PaymentAccount);

            Console.WriteLine($"Счет: {account1.Number}");
            Console.WriteLine($"Тип: {account1.AccountType}");
            Console.WriteLine($"Баланс: {account1.Balance:0.##}\r\n");

            var account2 = new BankAccount(AccountType.DepositAccount);

            Console.WriteLine($"Счет: {account2.Number}");
            Console.WriteLine($"Тип: {account2.AccountType}");
            Console.WriteLine($"Баланс: {account2.Balance:0.##}\r\n");

            Console.WriteLine("Положить на счет 500");
            account2.Deposit(500);
            Console.WriteLine($"Баланс: {account2.Balance:0.##}\r\n");

            Console.Write("Снять 300");
            Console.WriteLine($" - {(account2.Withdraw(300) ? "успешно снято 300" : "недостаточно средств")}");
            Console.WriteLine($"Баланс: {account2.Balance:0.##}\r\n");

            Console.Write("Снять 400");
            Console.WriteLine($" - {(account2.Withdraw(400) ? "успешно снято 400" : "недостаточно средств")}");
            Console.WriteLine($"Баланс: {account2.Balance:0.##}\r\n");


            var account3 = new BankAccount(3545.65m);

            Console.WriteLine($"Счет: {account3.Number}");
            Console.WriteLine($"Тип: {account3.AccountType}");
            Console.WriteLine($"Баланс: {account3.Balance:0.##}");

            Console.ReadKey();
        }
    }
}
