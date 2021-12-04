namespace ThreadPoolApp;

public static class StringExtension
{
    static object lockObject = new Object();

    public static void ThreadInfo(this string text, ConsoleColor threadColor = ConsoleColor.White, ConsoleColor infoColor = ConsoleColor.Yellow)
    {
        Random rnd = new Random();
        lock (lockObject)
        {
            Console.ForegroundColor = threadColor;
            Console.Write($"{DateTime.Now.ToString("hh:mm:ss.f")} [Thread{Thread.CurrentThread.ManagedThreadId}] ");
            Console.ForegroundColor = infoColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }

}
