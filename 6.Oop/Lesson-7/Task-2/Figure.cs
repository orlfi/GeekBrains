using Task_2.Enums;

namespace Task_2
{
    public abstract class Figure
    {
        protected int _x;

        public int X => _x;

        protected int _y;

        public int Y => _y;

        protected Colors _color;

        public Colors Color { get => _color; set => _color = value; }

        protected bool _visible;

        public bool Visible { get => _visible; set => _visible = value; }

        public void Move(int x, int y) => (_x, _y) = (x, y);

        public override string ToString() => $"Фигура: {GetType().Name} \r\nX={X}\r\nY={Y}\r\nColor={Color}\r\nVisible={Visible}";
    }
}