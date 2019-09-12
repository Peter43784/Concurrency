using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Synchronization
{
    class ThreadSafetyExample
    {
        // A field dedicated for the purpose of locking (such as _locker, in the example prior)
        // allows precise control over the scope and granularity of the lock.
        static readonly object _locker = new object();
        static int _val1 = 1, _val2 = 1;

        public static void ExecuteUnsafe(int times)
        {
            for (int i = 0; i < times; i++)
            {
                new Thread(GoUnsafe).Start();
            }
        }

        public static void ExecuteSafe(int times)
        {
            for (int i = 0; i < times; i++)
            {
                new Thread(GoSafe).Start();
            }
        }
        private static void GoUnsafe()
        {
            Console.WriteLine(_val1);
            _val1++;
        }

        private static void GoSafe()
        {
            lock (_locker)
            {
                Console.WriteLine(_val1);
                _val1++;
            }
        }


    }
}