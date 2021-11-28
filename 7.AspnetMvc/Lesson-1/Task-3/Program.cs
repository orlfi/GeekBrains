var thread = new Thread(() => DoWork());
thread.IsBackground = true;
thread.Start();
Thread.Sleep(2000);
thread.Interrupt(); // вызывает исключение в потоке
Console.WriteLine("End");
Console.ReadLine();

static void DoWork()
{
    while (true) // => где то внутри будет выброшено исключение ThreadInterruptedException
    {
        Console.Title = DateTime.Now.ToString("HH:mm:ss.fff");
        Thread.Sleep(100);
    }
}





