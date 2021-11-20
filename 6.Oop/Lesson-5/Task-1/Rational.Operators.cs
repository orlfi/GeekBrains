namespace Task_1
{
    public partial class Rational
    {
        public static Rational operator ++(Rational val) => val + new Rational(1, 1);

        public static Rational operator --(Rational val) => val - new Rational(1, 1);

        public static bool operator ==(Rational a, Rational b) => a.Equals(b);
        public static bool operator !=(Rational a, Rational b) => !a.Equals(b);

        public static bool operator >(Rational a, Rational b) => (double)a > (double)b;

        public static bool operator <(Rational a, Rational b) => (double)a < (double)b;

        public static bool operator >=(Rational a, Rational b) => a == b || a > b;

        public static bool operator <=(Rational a, Rational b) => a == b || a < b;

        public static Rational operator +(Rational a, Rational b) =>
            new Rational(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        public static Rational operator -(Rational a, Rational b) =>
            new Rational(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Rational operator *(Rational a, Rational b) =>
            new Rational(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Rational operator /(Rational a, Rational b) =>
            new Rational(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

        public static explicit operator double(Rational value) =>
            (double)value.Numerator / value.Denominator;

        public static explicit operator float(Rational value) =>
            (float)value.Numerator / value.Denominator;

        public static explicit operator int(Rational value) =>
            (int)value.Numerator / value.Denominator;

        public static double operator %(Rational a, Rational b) => (double)a % (double)b;
    }
}