using Task_2.Enums;

namespace Task_2
{
    public class Circle : Point, IEquatable<Circle>
    {
        public int Radius { get; set; }

        public Circle(int x, int y, int radius) : this(x, y, radius, Colors.white) { }

        public Circle(int x, int y, int radius, Colors color) : this(x, y, radius, color, true) { }

        public Circle(int x, int y, int radius, Colors color, bool visible) : base(x, y, color, visible) => Radius = radius;

        public double GetArea() => Math.PI * Radius * Radius;

        public override string ToString() => base.ToString() + $"\r\nПлощадь: {GetArea()}";

        public bool Equals(Circle? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                && this.Radius == other.Radius;
        }

        public override bool Equals(object? obj) => Equals(obj as Circle);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Radius.GetHashCode();
                return hashCode;
            }
        }
    }
}