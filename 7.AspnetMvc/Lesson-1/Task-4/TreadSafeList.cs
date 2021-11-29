using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_4
{
    public class TreadSafeList<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly object _lockObject = new object();

        public int Count
        {
            get
            {
                lock (_lockObject)
                {
                    return _list.Count;
                }
            }
        }
        public T this[int index]
        {
            get
            {
                lock (_lockObject)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _list[index] = value;
                }
            }
        }
        public void Add(T item)
        {
            lock (_lockObject)
            {
                _list.Add(item);
            }
        }

        public void Remove(T item)
        {
            lock (_lockObject)
            {
                _list.Remove(item);
            }
        }
    }
}