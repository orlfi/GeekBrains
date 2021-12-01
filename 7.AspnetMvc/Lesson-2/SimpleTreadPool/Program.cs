
var task = DoWork();
Console.WriteLine($"Main after DoWork {Thread.CurrentThread.ManagedThreadId}");
await task;
Console.WriteLine($"Main done! {Thread.CurrentThread.ManagedThreadId}");
Console.ReadLine();

static async Task DoWork()
{
    Console.WriteLine($"DoWork before TaskRun {Thread.CurrentThread.ManagedThreadId}");
    await Task.Run(() =>
    {
        Console.WriteLine($"In task { Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000);
    }).ConfigureAwait(true);
    Console.WriteLine($"DoWork after TaskRun {Thread.CurrentThread.ManagedThreadId}");
}