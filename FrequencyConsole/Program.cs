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
            var rand = new Random();
            var watch = new Stopwatch();

            var jsonString = "";

            using (var sr = new StreamReader(@"D:\VS Solutions\FrequencyCalculator\FrequencyConsole\bin\Debug\netcoreapp3.1\pairstest.json"))
            {
                jsonString = sr.ReadToEnd();
            }
            var valuesPassed = JsonConvert.DeserializeObject<List<List<int>>>(jsonString);

            var valuesToFind = new List<List<int>> { new List<int> { 14, 5 }, new List<int> { 9, 2 } };

            watch.Start();
            var results = valuesPassed.CalculatePairs(valuesToFind, false);
            foreach(var result in results)
            {
                Console.WriteLine($"{result.Item} - {result.Item2} ::: {result.Frequency}");
            }
            

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
