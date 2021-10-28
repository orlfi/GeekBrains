using System;

namespace Task_3
{
    public class BankAccount
    {
        private static int lastNumber = 0;
        public static int GenerateNumber()
        {
            return lastNumber++;
        }

        private int _number;

        private decimal _balance;

        private AccountType _type;

        public BankAccount() => _number = GenerateNumber();

        public BankAccount(decimal balance) => (_number, _balance) = (GenerateNumber(), balance);

        public BankAccount(AccountType type) => (_number, _type) = (GenerateNumber(), type);

        public BankAccount(decimal balance, AccountType type) => (_number, _balance, _type) = (GenerateNumber(), balance, type);

        public int GetNumber() => _number;

        public decimal GetBalance() => _balance;

        public AccountType GetAccountType() => _type;
    }
}