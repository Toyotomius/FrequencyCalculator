using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var watch = new Stopwatch();
            var rand = new Random();

            //string json = "";
            //using (var sr = new StreamReader("sortedlist.json"))
            //{
            //    json = sr.ReadToEnd();
            //}

            //var list = JsonConvert.DeserializeObject<List<int>>(json);
            //watch.Start();
            //var result = list.CalculateSingles<int>();
            //watch.Stop();
            //foreach(var itm in result)
            //{
            //    Console.WriteLine($"{itm.Item}  : {itm.Frequency}");
            //}

            //Console.WriteLine(watch.ElapsedMilliseconds);

            var json = "";

            using (var sw = new StreamReader("singlestest.json"))
            {
                json = sw.ReadToEnd();
            }

            var list = JsonConvert.DeserializeObject<List<int>>(json);

            var arr = new int[] { 2, 5 };

            watch.Start();
            var results = list.CalculateSingles<int>(arr, true);

            foreach(var result in results)
            {
                Console.WriteLine($"{result.Item} :: {result.Frequency}");
            }

            watch.Stop();

            Console.WriteLine("\n\n" + watch.ElapsedMilliseconds);

            Console.WriteLine("Breakpoint");

            //TODO: Set up check for binary search and nested collections.
        }
    }
}
