using System;

namespace Lesson_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount();
            account.SetNumber("0001");
            account.SetBalance(570435.43m);
            account.SetType(AccountType.PaymentAccount);

            Console.WriteLine(account.ToString());
            Console.ReadKey();
        }
    }
}
