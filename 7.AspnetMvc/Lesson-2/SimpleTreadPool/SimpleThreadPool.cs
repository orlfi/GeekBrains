using System.Collections.Concurrent;

namespace ThreadPoolApp;

public class SimpleThreadPool
{
    private readonly ConcurrentQueue<Action<ConsoleColor>> _actions = new ConcurrentQueue<Action<ConsoleColor>>();

    private readonly List<Thread> _threads;

    private readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

    private readonly int _threadCount;

    public SimpleThreadPool(int threadCount)
    {
        _threadCount = threadCount;
        _threads = new List<Thread>(threadCount);
        Initialize();
        StartThreads();
    }

    private void Initialize()
    {
        Random rnd = new Random();
        for (int i = 0; i < _threadCount; i++)
        {
            ConsoleColor color = (ConsoleColor)rnd.Next(1, 15);
            var thread = CreateThread(color);
            _threads.Add(thread);
            "Поток создан".ThreadInfo(color, color);
        }
    }

    private void StartThreads()
    {
        for (int i = 0; i < _threadCount; i++)
        {
            _threads[i].Start();
        }
    }

    public void EnQueue(Action<ConsoleColor> action)
    {
        _actions.Enqueue(action);
        resetEvent.Set();
    }

    private Thread CreateThread(ConsoleColor color = ConsoleColor.White)
    {
        var thread = new Thread(() => Worker(color));
        thread.IsBackground = true;
        return thread;
    }

    private void Worker(ConsoleColor color)
    {
        while (true)
        {
            "Поток ожидает задачу...".ThreadInfo(color, color);
            resetEvent.WaitOne();
            if (_actions.TryDequeue(out Action<ConsoleColor> action))
            {
                action(color);
            }
        }
    }
}


