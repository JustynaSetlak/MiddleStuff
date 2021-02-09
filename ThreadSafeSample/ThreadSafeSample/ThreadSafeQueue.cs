using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreadSafeSample
{
    public class ThreadSafeQueue<T>
    {
        private List<T> data = new List<T>();
        private static readonly object actionLock = new Object();

        public T Dequeue()
        {
            T item = default;

            lock (actionLock)
            {
                if (data.Any())
                {
                    item = data.First();
                    data.Remove(item);
                }
            }

            return item;
        }

        public void Enqueue(T entry)
        {
            lock (actionLock)
            {
                data.Add(entry);
            }
        }
    }
}
