using Task_2.Enums;

namespace Task_2
{
    public class Rect : Point
    {
        public Size Size { get; set; }

        public Rect(int x, int y, Size size) : this(x, y, size, Colors.white) { }

        public Rect(int x, int y, Size size, Colors color) : this(x, y, size, color, true) { }

        public Rect(int x, int y, Size size, Colors color, bool visible) : base(x, y, color, visible) => Size = size;

        public int GetArea() => Size.Width * Size.Height;

        public override string ToString() => base.ToString() + $"\r\nПлощадь: {GetArea()}";
    }
}