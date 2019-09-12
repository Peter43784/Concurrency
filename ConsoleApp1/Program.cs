using System;
using System.Threading;
using Threading;

namespace ConsoleApp1
{
    class Program
    {
        [ThreadStatic]
        static int value = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("Main thread Id:" + Thread.CurrentThread.ManagedThreadId);

            var a = new Thread(WriteY);

            //a.Start();

            #region A separate copy of the cycles variable is created on each thread's memory stack

            //Examples.RunMethodInMultipleThreads();

            #endregion

            #region How to share data between threads

            //var tst = new DataSharingExamples();
            //tst.RunStatic();
            //tst.RunInstance();

            //ThreadStatic example
            //void Act()
            //{
            //    value++;
            //    Console.WriteLine($"T-{Thread.CurrentThread.ManagedThreadId}: " + value);
            //}

            //Act();
            //new Thread(Act).Start();
            //new Thread(Act).Start();

            ////ThreadLocal<T> example  
            //ThreadLocal<int> local = new ThreadLocal<int>(() =>
            //{
            //    return 10;
            //});

            //void Act()
            //{
            //    local.Value++;
            //    Console.WriteLine($"T-{Thread.CurrentThread.ManagedThreadId}: " + local.Value);
            //}

            //new Thread(Act).Start();
            //new Thread(Act).Start();

            //local.Dispose();
            #endregion

            #region Sharing data between threads is primary cause of complexity in multithreaded environment

            //DataSharingExamples.RunStaticReverse();

            #endregion

            #region Data capturing

            //string text = "t1";
            //Thread t1 = new Thread(() => Console.WriteLine(text));


            //text = "t2";
            //Thread t2 = new Thread(() => Console.WriteLine(text));

            //t1.Start();
            //t2.Start();

            #endregion

            #region Foreground and background threads

            //TODO: By default, threads you create explicitly are foreground threads.
            //TODO: Foreground threads keep the application alive for as long as any one of them is running,
            //TODO: whereas background threads do not. Once all foreground threads finish, the application ends, 
            //TODO: and any background threads still running abruptly terminate.

            //a.IsBackground = true;
            //a.Start();

            #endregion

            #region Exception Handling

            //try
            //{
            //    //var t = new Thread(ExceptionHandlingExamples.Go);

            //    var t = new Thread(ExceptionHandlingExamples.GoImproved);
            //    t.IsBackground = true;
            //    t.Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception!");
            //}
            #endregion

            #region Thread Pool

            int workerThreads;
            int completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"max: {workerThreads}, maximum number of asynchronous I/O threads {completionPortThreads}");
            ThreadPool.QueueUserWorkItem(Write, completionPortThreads);

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"min: {workerThreads}, minimum number of asynchronous I/O threads that the thread pool creates on demand: {completionPortThreads}");

            ThreadPool.QueueUserWorkItem(Write, "Hello from the thread pool!");


            #endregion

            Console.WriteLine("Main method has been complete!");
        }

        static void WriteY()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("Worker thread Id:" + threadId);
            for (int i = 0; i < 5; i++)
            {
                Console.Write(threadId);
            }
            Console.WriteLine();
        }

        static void Write(object data)   // data will be null with the first call.
        {
            Console.WriteLine(data);
        }
    }
}
