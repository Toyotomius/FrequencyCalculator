using FrequencyCalculator;
using System;
using System.Collections.Generic;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var nullString = new List<string> { null, "1", "1" };
            
                

            var n = nullString.CalculateSingles<string>();

            foreach (var itm in n)
            {
                Console.WriteLine($"{itm.Item} : {itm.Frequency}");
            }
        }
    }
}
