//  * Создать класс Figure для работы с геометрическими фигурами. 
// В качестве полей класса задаются цвет фигуры, состояние «видимое/невидимое». 
// Реализовать операции: передвижение геометрической фигуры по горизонтали, 
// по вертикали, изменение цвета, опрос состояния (видимый/невидимый). 
// Метод вывода на экран должен выводить состояние всех полей объекта. 
// Создать класс Point (точка) как потомок геометрической фигуры. 
// Создать класс Circle (окружность) как потомок точки. 
// В класс Circle добавить метод, который вычисляет площадь окружности. 
// Создать класс Rectangle (прямоугольник) как потомок точки, реализовать метод вычисления площади прямоугольника. 
// Точка, окружность, прямоугольник должны поддерживать методы передвижения по горизонтали и вертикали, изменения цвета.
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

        public override string ToString()
        {
            return $"Фигура: {GetType().Name} \r\nX={X}\r\nY={Y}\r\nColor={Color}\r\nVisible={Visible}";
        }
    }
}