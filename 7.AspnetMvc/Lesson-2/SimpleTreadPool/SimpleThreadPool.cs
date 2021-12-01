using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTreadPool
{
    public class SimpleThreadPool
    {
        private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
        private readonly List<Thread> _treads;

        public SimpleThreadPool(int threadCount)
        {
            _treads = new List<Thread>(threadCount);
            for (int i = 0; i < threadCount; i++)
            {
                var thread = CreateThread();
                _treads.Add(thread);
            }
        }

        public void EnQueue(Action action)
        {
            _actions.Append(action);
        }

        private Thread CreateThread()
        {
            var thread = new Thread(() => Worker());
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        private void Worker()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
            while (true)
            {

            }
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} stoped");
        }
    }
}