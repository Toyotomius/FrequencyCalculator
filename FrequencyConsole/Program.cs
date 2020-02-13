using System;
using System.Collections.Generic;
using FrequencyCalculator;

namespace FrequencyConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var testList = new List<string> { "2.2", "2.2", "3.1", "3.1", "1.2", "1.2", "1.2" };
            ICalculateFrequency<string> freqCalc = new CalculateSingleFrequency<string>();
            var result = freqCalc.CalculateSingles(testList);

            foreach(var itm in result)
            {
                Console.WriteLine($"{itm.Item} Number : {itm.Frequency} Frequency");
            }

        }
    }
}
