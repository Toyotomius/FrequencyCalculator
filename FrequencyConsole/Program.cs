using FrequencyCalculator.IEnumerableExtensions;
using System.Collections.Generic;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new List<List<string>>
                {
                    new List<string> { "1", "2","3" },
                    new List<string> { "1", "2","3","4" },
                    new List<string> { "1", "2", null },
                    null
                };

            var results = test.CalculatePairs<string>();

            System.Console.WriteLine("Breakpoint");
        }
    }
}
