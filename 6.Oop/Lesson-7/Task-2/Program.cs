using Task_2;
using Task_2.Enums;

var figures = new List<Figure>(3)
{
    new Point(10, 10),
    new Circle(20, 20, 30),
    new Rect(30, 20, new Size(10, 20), Colors.green)
};

foreach (var item in figures)
{
    Console.WriteLine(item);
    Console.WriteLine();
}

Console.WriteLine("Перемещение фигуры:");
var figure = figures[2];
figure.Move(50, 60);
Console.WriteLine(figure);
Console.WriteLine();

Console.WriteLine("Перемещение и изменение цвета фигуры:");
figure = figures[1];
figure.Move(50, 60);
figure.Color = Colors.green;
Console.WriteLine(figure);
