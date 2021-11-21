using Task_1;

var account1 = new BankAccount(1000.23m, AccountType.PaymentAccount);

Console.WriteLine(account1);

var account2 = new BankAccount(AccountType.DepositAccount);

Console.WriteLine(account2);

Console.WriteLine(account1 == account2);

Console.WriteLine(account1 != account2);

Console.WriteLine($"Hash счета {account1.Number}: {account1.GetHashCode()}");

Console.WriteLine($"Hash счета {account2.Number}: {account2.GetHashCode()}");

Console.ReadKey();