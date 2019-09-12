using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Synchronization
{
    class DeadLockExample
    {
        public static void Run()
        {
            object locker1 = new object();
            object locker2 = new object();

            new Thread(() => {
                lock (locker1)
                {
                    Thread.Sleep(1000);
                    lock (locker2) // Deadlock
                    {
                        Console.WriteLine("Finished Thread 1");
                    }
                }
            }).Start();

            lock (locker2)
            {
                Thread.Sleep(1000);
                lock (locker1)                          // Deadlock
                {
                    Console.WriteLine("Finished Thread 2");
                }
            }
        } 
    }
}
