using System;

namespace Task_1
{
    public class BankAccount
    {

        private int _number;

        private decimal _balance;

        private AccountType _type;

        public void SetNumber(int number) => _number = number;

        public int GetNumber() => _number;

        public void SetBalance(decimal balance) => _balance = balance;

        public decimal GetBalance() => _balance;

        public void SetAccountType(AccountType type) => _type = type;

        public AccountType GetAccountType() => _type;

        public override string ToString()
        {
            return $"Счет {_number} ({_type.ToString()}): {_balance:0.##}";
        }
    }
}