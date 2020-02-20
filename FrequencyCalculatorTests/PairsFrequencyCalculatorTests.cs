using FrequencyCalculator.IEnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class PairsFrequencyCalculatorTests
    {
        [Fact]
        public void CalculatePairsFrequency_ShouldCalculateWhenPassed_NestedIntList()
        {
            var nestedInt = new List<List<int>>
            {
                new List<int> { 1,2 },
                new List<int> { 1,2 }
            };

            var actual = nestedInt.CalculatePairs();

            Assert.Contains(actual, x => (x.Item == 2 && x.Item2 == 1) && x.Frequency == 2);
        }
    }
}
