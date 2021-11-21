using System.Diagnostics.CodeAnalysis;

namespace Task_2
{
    public readonly struct Size : IEquatable<Size>
    {
        public int Width { get; }
        public int Height { get; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator ==(Size a, Size b) => a.Equals(b);

        public static bool operator !=(Size a, Size b) => !a.Equals(b);

        public bool Equals(Size other)
        {
            return this.Width == other.Width
                && this.Height == other.Height;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals(obj is Size);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Width.GetHashCode();
                return (hashCode * 397) ^ Height.GetHashCode();
            }
        }
    }
}