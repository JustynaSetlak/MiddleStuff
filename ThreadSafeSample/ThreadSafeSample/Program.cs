using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var minimumValue = 0;
            var maximumValue = 30;
            var maximumDegreeOfParallelism = 3;

            var data = new ThreadSafeQueue<int>();

            var integerList = Enumerable.Range(minimumValue, maximumValue).ToList();

            Parallel.ForEach(integerList, new ParallelOptions { MaxDegreeOfParallelism = maximumDegreeOfParallelism }, specificValue =>
            {
                data.Enqueue(specificValue);
                Console.Write($"\n Enque {specificValue}, threadId: {Thread.CurrentThread.ManagedThreadId}"); 
                
                if(specificValue%2 == 0)
                {
                    var value = data.Dequeue();
                    Console.Write($"\n Deque {value}, threadId: {Thread.CurrentThread.ManagedThreadId}");
                }
            });

            Console.ReadKey();
        }
    }
}
