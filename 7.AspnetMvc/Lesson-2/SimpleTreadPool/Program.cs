using ThreadPoolApp;

Random rnd = new Random();
SimpleThreadPool pool = new SimpleThreadPool(3);
for (int i = 1; i < 7; i++)
{
    pool.EnQueue((threadColor) =>
    {
        int taskNumber = i;
        ConsoleColor color = GetNewColor(threadColor, rnd);
        $"Запуск выполнения Задачи №{taskNumber}".ThreadInfo(threadColor, color);
        Thread.Sleep(3000);
        $"Окончание работы Задачи №-{taskNumber}".ThreadInfo(threadColor, color);
    });
    Thread.Sleep(1000);
}
Console.ReadLine();

static ConsoleColor GetNewColor(ConsoleColor color, Random rnd)
{
    ConsoleColor result;
    while (true)
    {
        result = (ConsoleColor)rnd.Next(1, 15);
        if (result != color)
            return result;
    }
}