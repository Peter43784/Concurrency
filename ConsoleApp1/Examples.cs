using System;
using System.Threading;

namespace Threading
{
    class Examples
    {
        /// <summary>
        /// Each thread will get separate copy of local variable(i) 
        /// </summary>
        public static void RunMethodInMultipleThreads()
        {
            // Initialization of new thread 
            var t = new Thread(() => WriteWorkerThreadId());

            // Start method execution in separate thread
            t.Start();

            // Execute method in caller thread
            WriteWorkerThreadId();
        }

        static void WriteWorkerThreadId(int times = 5)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("Worker thread Id's:");

            for (int i = 0; i < times; i++)
            {
                Console.Write(threadId);
            }

            Console.WriteLine();
        }
    }
}
