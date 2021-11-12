using Task_2;
using Task_2.Enums;

var figures = new List<Figure>(3)
{
    new Point(10, 10),
    new Circle(20, 20, 30),
    new Rect(30, 20, new Size(10, 20), Colors.green)
};

foreach (var figure in figures)
{
    Console.WriteLine(figure);
    Console.WriteLine();
}

figures[2].Move(50, 60);
Console.WriteLine(figures[2]);
