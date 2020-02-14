using FrequencyCalculator;
using System;
using System.Collections.Generic;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var firstList = new List<int> { 0, 0 };
            var secondList = new List<int> { 0, 0 };
            var nestedlist = new List<List<int>> { firstList, secondList };


            IIndividualFrequency<List<int>> freqCalc = new CalculateIndividualFrequency<List<int>>();
            var result = freqCalc.CalculateSingles(nestedlist);

            foreach (var itm in result)
            {
                Console.WriteLine($"{itm.Item} Number : {itm.Frequency} Frequency");
            }
        }
    }
}
