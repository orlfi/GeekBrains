using System;
namespace Task_1
{
    public partial class Rational : IEquatable<Rational>
    {
        private readonly int _numerator;
        private readonly int _denominator;

        /// <summary>Числитель</summary>
        public int Numerator => _numerator;

        /// <summary>Знаменатель</summary>
        public int Denominator => _denominator;

        public Rational(int numerator, int denominator) => (_numerator, _denominator) = (numerator, denominator);

        public bool Equals(Rational other)
        {
            if (other is null) return false;
            return other.Numerator == Numerator
                && other.Denominator == Denominator;
        }

        public override bool Equals(object obj) => Equals(obj as Rational);

        public override int GetHashCode()
        {
            var hashCode = Numerator.GetHashCode();
            hashCode = (hashCode * 397) ^ Denominator.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}