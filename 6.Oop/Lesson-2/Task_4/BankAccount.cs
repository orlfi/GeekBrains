using System;

namespace Task_4
{
    public class BankAccount
    {
        private static int lastNumber = 0;

        public static int GenerateNumber() => lastNumber++;

        private int _number;

        private decimal _balance;

        private AccountType _type;

        public int Number { get => _number; }

        public decimal Balance { get => _balance; set => _balance = value; }

        public AccountType AccountType { get => _type; set => _type = value; }

        public BankAccount() => _number = GenerateNumber();

        public BankAccount(decimal balance) => (_number, _balance) = (GenerateNumber(), balance);

        public BankAccount(AccountType type) => (_number, _type) = (GenerateNumber(), type);

        public BankAccount(decimal balance, AccountType type) => (_number, _balance, _type) = (GenerateNumber(), balance, type);
    }
}