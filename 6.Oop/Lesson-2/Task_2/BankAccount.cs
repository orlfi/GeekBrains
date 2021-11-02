using System;

namespace Task_2
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

        public void SetNumber() => _number = GenerateNumber();

        public int GetNumber() => _number;

        public void SetBalance(decimal balance) => _balance = balance;

        public decimal GetBalance() => _balance;

        public void SetType(AccountType type) => _type = type;

        public AccountType GetAccountType() => _type;
    }
}