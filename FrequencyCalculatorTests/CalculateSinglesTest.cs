using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class CalculateIndividualSinglesTests
    {
        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntArray()
        {
            var calcSingleFrequency = CreateDefaultSinglesCalculator<int>();
            var intArray = new int[] { 0, 0 };

            var actual = calcSingleFrequency.CalculateSingles(intArray);

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntList()
        {
            var calcSingleFrequency = CreateDefaultSinglesCalculator<int>();
            var intList = new List<int>() { 0, 0 };

            var actual = calcSingleFrequency.CalculateSingles(intList);

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatStringList()
        {
            var calcSingleFrequency = CreateDefaultSinglesCalculator<string>();
            var stringList = new List<string> { "0", "0" };

            var actual = calcSingleFrequency.CalculateSingles(stringList);

            Assert.Contains(actual, x => x.Item == "0" && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldThrowErrorWhenPassed_ListOfIntLists()
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<List<int>>();
            var nestedIntList = new List<List<int>>() {
                                                            new List<int> { 0, 0 },
                                                            new List<int> { 0, 0 }
                                                       };

            Assert.Throws<System.ArgumentException>(() => calculateSingleFrequency.CalculateSingles(nestedIntList));
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldThrowErrorWhenPassed_ListOfStringLists()
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<List<string>>();
            var nestedStringList = new List<List<string>>() {
                                                                new List<string> {"0", "0" },
                                                                new List<string> {"0", "0" }
                                                            };

            Assert.Throws<System.ArgumentException>(() => calculateSingleFrequency.CalculateSingles(nestedStringList));
        }

        private IIndividualFrequency<T> CreateDefaultSinglesCalculator<T>()
        {
            return new CalculateIndividualFrequency<T>();
        }
    }
}
