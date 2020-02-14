using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class CalculateNestedSinglesTests
    {
        [Fact]
        public void CalculateNestedSingles_ShouldCalculateWhenPassed_ListOfIntLists()
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<int>();
            var nestedIntList = new List<List<int>>() {
                                                            new List<int> { 0, 0 },
                                                            new List<int> { 0, 0 }
                                                       };

            var actual = calculateSingleFrequency.CalculateNestedSingles(nestedIntList);

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 4);
        }
        
        private IIndividualFrequency<T> CreateDefaultSinglesCalculator<T>()
        {
            return new CalculateIndividualFrequency<T>();
        }

    }
}
