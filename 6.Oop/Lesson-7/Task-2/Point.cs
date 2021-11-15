using Task_2.Enums;

namespace Task_2
{
    public class Point : Figure
    {
        public Point(int x, int y) => (_x, _y) = (x, y);

        public Point(int x, int y, Colors color) : this(x, y) => _color = color;

        public Point(int x, int y, Colors color, bool visible) : this(x, y, color) => _visible = visible;
    }
}