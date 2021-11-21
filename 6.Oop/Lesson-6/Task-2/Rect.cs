using Task_2.Enums;

namespace Task_2
{
    public class Rect : Point, IEquatable<Rect>
    {
        public Size Size { get; }

        public Rect(int x, int y, Size size) : this(x, y, size, Colors.white) { }

        public Rect(int x, int y, Size size, Colors color) : this(x, y, size, color, true) { }

        public Rect(int x, int y, Size size, Colors color, bool visible) : base(x, y, color, visible) => Size = size;

        public int GetArea() => Size.Width * Size.Height;

        public override string ToString() => base.ToString() + $"\r\nПлощадь: {GetArea()}";

        public bool Equals(Rect? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                && this.Size == other.Size;
        }

        public override bool Equals(object? obj) => Equals(obj as Rect);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Size.GetHashCode();
                return hashCode;
            }
        }
    }
}