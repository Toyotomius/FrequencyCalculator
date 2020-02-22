using FrequencyCalculator.IEnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var watch = new Stopwatch();
            var rand = new Random();
            var list = new List<string>();
            for (var i = 0; i < 200000; i++)
            {
                list.Add(rand.Next(0, 101).ToString());
            }

            list.Sort();

            watch.Start();
            var results = list.CalculateSingles<string>("99", true);
            watch.Stop();

            var result = list.CalculateSingles<string>("99", false);

            var totalResult = list.CalculateSingles<string>();

            Console.WriteLine(watch.ElapsedMilliseconds);

            Console.WriteLine($"{results.Item}  : {results.Frequency}");
            Console.WriteLine($"{result.Item}  : {result.Frequency}");
            foreach(var itm in totalResult)
            {
                Console.WriteLine($"{itm.Item}  : {itm.Frequency}");
            }

            System.Console.WriteLine("Breakpoint");

            //TODO: Set up check for binary search and nested collections.
        }
    }
}
