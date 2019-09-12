using System;

namespace ConsoleApp1
{
    class ExceptionHandlingExamples
    {
        public static void Go() { throw null; }

        public static void GoImproved()
        {
            try
            {
                // ...
                throw null; // The NullReferenceException will get caught below
                // ...
            }
            catch (Exception ex)
            {
                // Typically log the exception, and/or signal another thread
                // that we've come unstuck
                // ...

                Console.WriteLine("Exception!");
            }
            finally
            {
                Console.WriteLine("In finally");
            }
        }
    }
}
