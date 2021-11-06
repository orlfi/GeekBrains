using System;

namespace Task_2
{
    public struct Complex : IEquatable<Complex>
    {
        private double _a;
        private double _b;

        public double A => _a;
        public double B => _b;

        public Complex(double a, double b) => (_a, _b) = (a, b);

        public static Complex operator +(Complex n, Complex m) => new Complex(n.A + m.A, n.B + m.B);
        public static Complex operator -(Complex n, Complex m) => new Complex(n.A - m.A, n.B - m.B);
        public static Complex operator *(Complex n, Complex m) => new Complex(n.A * m.A - n.B * m.B, n.A * m.B + n.B * m.A);
        public static Complex operator /(Complex n, Complex m)
        {
            var z = new Complex(m.A, m.B * -1);
            var nominator = n * z;
            var denominator = m.A * m.A + m.B * m.B;
            return new Complex(nominator.A / denominator, nominator.B / denominator);
        }

        public static bool operator ==(Complex n, Complex m) => Equals(n, m);
        public static bool operator !=(Complex n, Complex m) => !Equals(n, m);

        public bool Equals(Complex other)
        {
            return A == other.A
                && B == other.B;
        }

        public override bool Equals(object obj) => obj is null ? false : Equals((Complex)obj);

        public override int GetHashCode()
        {
            var hashCode = A.GetHashCode();
            hashCode = (hashCode * 397) ^ B.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", _a == 0 ? "" : _a, (_b > 0 ? (_a == 0 ? "" : "+") : ""), (_b == 0 ? "" : $"{_b}j"));
        }

    }
}