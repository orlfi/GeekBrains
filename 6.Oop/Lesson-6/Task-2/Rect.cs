using Task_2.Enums;

namespace Task_2
{
    public class Rect : Point
    {
        public Size Size { get; set; }

        public Rect(int x, int y, Size size) : base(x, y)
        {
            Size = size;
        }

        public Rect(int x, int y, Size size, Colors color) : base(x, y, color)
        {
            Size = size;
        }

        public Rect(int x, int y, Size size, Colors color, bool visible) : base(x, y, color, visible)
        {
            Size = size;
        }

        public int GetArea() => Size.Width / Size.Height;
    }
}