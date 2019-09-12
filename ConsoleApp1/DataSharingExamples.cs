using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class DataSharingExamples
    {
        static bool done;    // Static fields are shared between all threads
        bool finished;

        /// <summary>
        /// One way to share data across the threads is to use static variables
        /// </summary>
        public void RunStatic()
        {
            void Act()
            {
                if (done) return;
                done = true;
                Console.WriteLine("Done");
            }

            new Thread(Act).Start();
            new Thread(Act).Start();
        }

        /// <summary>
        /// Another way to share data across the threads is to use instance variables
        /// </summary>
        public void RunInstance()
        {
            void Act()
            {
                if (finished) return;
                finished = true;
                Console.WriteLine("Finished");
            }

            new Thread(Act).Start();
            new Thread(Act).Start();
        }

        public static void RunStaticReverse()
        {
            void Act()
            {
                if (done) return;
                Console.WriteLine("Done");
                done = true;
            }

            new Thread(Act).Start();
            Act();
        }
    }
}
