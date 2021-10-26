using System;

namespace Lesson_2
{
    public class BankAccount
    {
        private string _number;

        private decimal _balance;

        private AccountType _type;

        public void SetNumber(string number) => _number = number;

        public string GetNumber() => _number;

        public void SetBalance(decimal balance) => _balance = balance;

        public decimal GetBalance() => _balance;

        public void SetType(AccountType type) => _type = type;

        public AccountType GetType() => _type;

        public override string ToString()
        {
            return $"Счет {_number} ({_type.ToString()}: {_balance:5.2})";
        }
    }
}