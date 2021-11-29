using Task_4;
public class Program
{
    // static List<int> list = new List<int>();
    static readonly TreadSafeList<int> treadSafeList = new TreadSafeList<int>();
    static readonly List<Task> tasks = new List<Task>();
    static void Main(string[] args)
    {
        // var worker = new Worker();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(new Task(() =>
            {
                for (int j = 0; j < 100; j++)
                {
                    treadSafeList.Add(j);
                    // list.Add(j); возникают косяки!!!
                }
            }));
            tasks[i].Start();
        }
        await Task.WhenAll(tasks);
        Console.WriteLine("End");
    }
}




