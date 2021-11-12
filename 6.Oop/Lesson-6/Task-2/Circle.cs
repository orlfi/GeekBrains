using Task_2.Enums;

namespace Task_2
{
    public class Circle : Point
    {
        public int Radius { get; set; }

        public Circle(int x, int y, int radius) : this(x, y, radius, Colors.white) { }

        public Circle(int x, int y, int radius, Colors color) : this(x, y, radius, color, true) { }

        public Circle(int x, int y, int radius, Colors color, bool visible) : base(x, y, color, visible) => Radius = radius;

        public double GetArea() => Math.PI * Radius * Radius;

        public override string ToString() => base.ToString() + $"\r\nПлощадь: {GetArea()}";
    }
}