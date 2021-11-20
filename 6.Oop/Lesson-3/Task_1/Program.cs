using System;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var account1 = new BankAccount(1000.23m, AccountType.PaymentAccount);

            Console.WriteLine($"Счет: {account1.Number}");
            Console.WriteLine($"Тип: {account1.AccountType}");
            Console.WriteLine($"Баланс: {account1.Balance:0.##}\r\n");

            var account2 = new BankAccount(AccountType.DepositAccount);

            Console.WriteLine($"Счет: {account2.Number}");
            Console.WriteLine($"Тип: {account2.AccountType}");
            Console.WriteLine($"Баланс: {account2.Balance:0.##}\r\n");

            Console.Write($"Перевод со счета {account1.Number} на счет {account2.Number} сумму 500: ");
            var result = account2.TransferFromAccount(account1, 500);
            if (result.Success)
            {
                Console.WriteLine($" успешно переведено 500");
            }
            else
            {
                Console.WriteLine(result.Error.Message);
            }
            Console.WriteLine($"Баланс счета {account1.Number}: {account1.Balance:0.##}");
            Console.WriteLine($"Баланс счета {account2.Number}: {account2.Balance:0.##}\r\n");


            Console.Write($"Перевод со счета {account1.Number} на счет {account2.Number} сумму 600: ");
            var result2 = account2.TransferFromAccount(account1, 600);
            if (result2.Success)
            {
                Console.WriteLine($"Успешно переведено 500");
            }
            else
            {
                Console.WriteLine(result2.Error.Message);
            }
            Console.WriteLine($"Баланс счета {account1.Number}: {account1.Balance:0.##}");
            Console.WriteLine($"Баланс счета {account2.Number}: {account2.Balance:0.##}\r\n");

            Console.ReadKey();
        }
    }
}
