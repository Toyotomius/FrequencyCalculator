using FrequencyCalculator;
using System;
using System.Collections.Generic;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new List<List<int>>
            {
                    new List<int> { 0, 0 },
                    new List<int> { 0, 0 }
            };

            var n = test.CalculateSingles<int>();

            foreach (var itm in n)
            {
                Console.WriteLine($"{itm.Item} : {itm.Frequency}");
            }
        }
    }
}
