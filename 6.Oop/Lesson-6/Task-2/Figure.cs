using Task_2.Enums;

namespace Task_2
{
    public abstract class Figure : IEquatable<Figure>
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

        public override string ToString() =>
            $"Фигура: {GetType().Name} \r\nX={X}\r\nY={Y}\r\nColor={Color}\r\nVisible={Visible}";

        public bool Equals(Figure? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return _x == other._x
                && _y == other._y
                && _color == other._color
                && _visible == other._visible;
        }

        public override bool Equals(object? obj) => Equals(obj as Figure);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _x.GetHashCode();
                hashCode = (hashCode * 397) ^ _y.GetHashCode();
                hashCode = (hashCode * 397) ^ _color.GetHashCode();
                hashCode = (hashCode * 397) ^ _visible.GetHashCode();
                return hashCode;
            }
        }
    }
}