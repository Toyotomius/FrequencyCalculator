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

            var list = new List<List<string>> { new List<string> { "1", "2"},
                                                new List<string> {"1", "2"} };

            var test = new List<string> { "1", null };

            var results = list.CalculatePairs(test);
            Console.WriteLine(results.Item);
            

            Console.WriteLine("Breakpoint");

            //TODO: Set up check for binary search and nested collections.
        }
    }
}
