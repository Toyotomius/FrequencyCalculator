using System.Collections.Generic;
using System.Linq;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var nestInt = new List<List<int>>
            {
                new List<int>{ 1, 2 },
                new List<int>{ 1, 2 }
            };

            var distinct = nestInt.SelectMany(x => x).Distinct();

            System.Console.WriteLine("Breakpoint");
        }
    }
}
