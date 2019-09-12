using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Parallelism
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(3, 100000 - 3);

            #region PLINQ

            var parallelQuery =
                from n in numbers.AsParallel()
                where IsPrime(n)
                select n;

            int[] primes = parallelQuery.ToArray();

            Console.WriteLine(primes.Length);

            #endregion

            #region Parallel ForEach
            var misspellings = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, (n) =>
            {
                if (IsPrime(n))
                {
                    misspellings.Add(n);
                }
            });

            //Console.WriteLine(misspellings.Count);

            #endregion

            #region Parallel Invoke

            var data = new ConcurrentBag<string>();
            Parallel.Invoke(
                () => data.Add(DownloadString("http://www.foo.com")),
                () => data.Add(DownloadString("http://www.far.com")));


            #endregion

            #region Task Paralleslism

            var urls = new List<string>
            {
                "http://www.far.com",
                "http://www.foo.com"
            };
            var httpClient = new HttpClient();
            // Define what we're going to do for each URL.
            var downloads = urls.Select(url => httpClient.GetStringAsync(url));
            // Note that no tasks have actually started yet
            // because the sequence is not evaluated.
            // Start all URLs downloading simultaneously.
            Task<string>[] downloadTasks = downloads.ToArray();
            // Now the tasks have all started.
            // Asynchronously wait for all downloads to complete.
            string[] htmlPages = await Task.WhenAll(downloadTasks);

            #endregion


        }

        public static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            else if (number % 2 == 0)
                return number == 2;

            long N = (long)(Math.Sqrt(number) + 0.5);

            for (int i = 3; i <= N; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static string DownloadString(string url)
        {
            return new WebClient().DownloadString(url);
        }

    }
}
