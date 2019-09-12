using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;

namespace Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Thread Safety

            //ThreadSafetyExample.ExecuteUnsafe(4);

            //ThreadSafetyExample.ExecuteSafe(10);

            #endregion

            #region DeadLock

            //DeadLockExample.Run();

            #endregion

            #region Immutable colletions

            //var list = ImmutableList.CreateBuilder<int>().ToImmutable();

            //var l1 = list.Add(1);
            //var l2 = list.Add(2);

            #endregion

            #region Concurrent collections

            var timer = new Stopwatch();
            timer.Start();

            //Due to inserting a node into linked list
            //Reading from a ConcurrentDictionary, however, is fast because reads are lock-free.
            var count = 1000000;
            var d = new ConcurrentDictionary<int, int>();
            for (int i = 0; i < count; i++) d[i] = 123;

            Console.WriteLine("Write ConcurrentDictionary: " + timer.ElapsedMilliseconds);
            timer.Restart();

            for (int i = 0; i < count; i++)
            {
                var a = d[i];
            };

            Console.WriteLine("Read ConcurrentDictionary: " + timer.ElapsedMilliseconds);
            timer.Restart();

            var d1 = new Dictionary<int, int>();

            for (int i = 0; i < count; i++) lock (d1) d1[i] = 123;

            Console.WriteLine("Write Dictionary with lock: " + timer.ElapsedMilliseconds);

            timer.Restart();

            for (int i = 0; i < count; i++)
            {
                lock (d1)
                {
                    var a = d1[i];
                }
            };

            Console.WriteLine("Read Dictionary with lock: " + timer.ElapsedMilliseconds);

            #endregion

            Console.WriteLine("Hello World!");
        }
    }
}
