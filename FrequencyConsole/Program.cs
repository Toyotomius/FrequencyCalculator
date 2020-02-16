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
                null
            };

            IIndividualFrequency<int> freqCalc = new CalculateIndividualFrequency<int>();

            var n = freqCalc.CalculateSingles(test);

            foreach (var itm in n)
            {
                Console.WriteLine($"{itm.Item} : {itm.Frequency}");
            }
        }
    }
}
