using System;
using System.Collections;
using System.Collections.Generic;

namespace Task_1
{
    public class BankAccount : IEquatable<BankAccount>
    {
        private static int lastNumber = 0;

        public static int GenerateNumber() => lastNumber++;

        private readonly int _number;

        private readonly AccountType _type;

        private decimal _balance;

        public int Number { get => _number; }

        public AccountType AccountType { get => _type; }

        public decimal Balance { get => _balance; set => _balance = value; }

        public BankAccount() => _number = GenerateNumber();

        public BankAccount(decimal balance) => (_number, _balance) = (GenerateNumber(), balance);

        public BankAccount(AccountType type) => (_number, _type) = (GenerateNumber(), type);

        public BankAccount(decimal balance, AccountType type) => (_number, _balance, _type) = (GenerateNumber(), balance, type);

        public Result<decimal> TransferFromAccount(BankAccount source, decimal value)
        {
            var withdrawResult = source.Withdraw(value);
            if (withdrawResult.Success)
            {
                Deposit(value);
                return new Result<decimal>() { Data = Balance, Success = true };
            }
            return new Result<decimal>() { Success = false, Error = withdrawResult.Error };
        }

        private void Deposit(decimal value) => Balance += value;

        private Result<decimal> Withdraw(decimal value)
        {
            if (Balance - value > 0)
            {
                Balance -= value;
                return new Result<decimal>() { Data = Balance, Success = true };
            }

            return new Result<decimal>() { Success = false, Error = "Недостаточно средств" };
        }

        public static bool operator ==(BankAccount val1, BankAccount val2) => val1.Equals(val2);
        public static bool operator !=(BankAccount val1, BankAccount val2) => !val1.Equals(val2);

        public override bool Equals(object? obj) => Equals(obj as BankAccount);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = _number.GetHashCode();
                hash = (hash * 397) ^ _type.GetHashCode();
                hash = (hash * 397) ^ _balance.GetHashCode();
                return hash;
            }
        }

        public override string? ToString()
        {
            return $"Cчет {Number} ({AccountType.ToString()}): {Balance:0.##}";
        }

        public bool Equals(BankAccount? other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return _number == other._number
                && _type == other._type
                && _balance == other._balance;
        }
    }
}