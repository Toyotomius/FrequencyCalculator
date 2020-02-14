using FrequencyCalculator;
using System;
using System.Collections.Generic;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new List<List<string>> {  null  };

            IIndividualFrequency<string> freqCalc = new CalculateIndividualFrequency<string>();
            var result = freqCalc.CalculateNestedSingles(test);

            foreach (var itm in result)
            {
                Console.WriteLine($" Number is {itm.Item} : Frequency is {itm.Frequency} ");
            }
        }
    }
}
