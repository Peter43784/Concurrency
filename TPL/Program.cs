using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main thread:" + Thread.CurrentThread.ManagedThreadId);

            #region Creating and running Task

            //// Construct an unstarted task
            //Task t1 = new Task(ShowThreadId, "new Task");
            //t1.Start();
            ////blocking, try to avoid this
            //t1.Wait();

            //Task t2 = Task.Factory.StartNew(ShowThreadId, "Task.Factory.StartNew");
            //t2.Wait();

            //TaskScheduler Represents an object that handles the low-level work of queuing tasks onto threads.
            //TaskCreationOptions Specifies flags that control optional behavior for the creation and execution of tasks.
            //The same AS:
            //Task.Factory.StartNew(ShowThreadId, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            //Task t3 = Task.Run(() => ShowThreadId("Task.Run"));
            //t3.Wait();
            #endregion

            #region async await

            //// async methods are run on separate thread - that is false.
            //// methods marked by await are run on a separate thread. - false.
            //// task continuation will be executed on a separate thread. - Not always
            //await DelayAsync();

            //Console.WriteLine("SynchronizationContext: " + SynchronizationContext.Current);

            #endregion

            #region Exception handling

            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            Console.WriteLine("Started");
            try
            {
                 DelayAndThrowAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occured: " + e.Message);
            }
            Console.WriteLine("Finished");

            await Task.Delay(1000);
            GC.Collect();

            #endregion

            #region Canceletion tokens

            //var cts = new CancellationTokenSource();
            //var task = CancelableMethodAsync(cts.Token);
            //// At this point, the operation has been started.
            //// Issue the cancellation request.

            //cts.Cancel();

            //try
            //{
            //    await task;
            //    // If we get here, the operation completed successfully
            //    // before the cancellation took effect.
            //}
            //catch (OperationCanceledException)
            //{
            //    // If we get here, the operation was canceled before it completed.
            //    Console.WriteLine("Canceled");
            //}
            //catch (Exception)
            //{
            // If we get here, the operation completed with an error
            // before the cancellation took effect.
            //    throw;
            //}

            #endregion
        }

        private static async Task CancelableMethodAsync(CancellationToken ctsToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), ctsToken);
        }

        public static void ShowThreadId(object obj)
        {
            Console.WriteLine(obj + " run on thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task DelayAsync()
        {
            Console.WriteLine("Before await: " + Thread.CurrentThread.ManagedThreadId);
            //await Task.Delay(100);
            await Task.FromResult(100);

            Console.WriteLine("After await: " + Thread.CurrentThread.ManagedThreadId);
        }

        private async static Task DelayAndThrowAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException();
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
