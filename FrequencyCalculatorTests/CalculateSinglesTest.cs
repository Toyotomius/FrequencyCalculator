using System.Collections.Generic;
using Xunit;
using FrequencyCalculator.Tests.TestData;

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
        public void CalculateSinglesFrequency_ShouldThrowErrorWhenInstatiatedWith_ListOfIntLists()
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<List<int>>();
            var nestedIntList = new List<List<int>>() {
                                                            new List<int> { 0, 0 },
                                                            new List<int> { 0, 0 }
                                                       };

            Assert.Throws<System.InvalidCastException>(() => calculateSingleFrequency.CalculateSingles(nestedIntList));
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldThrowErrorWhenInstantiatedWith_ListOfStringLists()
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<List<string>>();
            var nestedStringList = new List<List<string>>() {
                                                                new List<string> {"0", "0" },
                                                                new List<string> {"0", "0" }
                                                            };

            Assert.Throws<System.InvalidCastException>(() => calculateSingleFrequency.CalculateSingles(nestedStringList));
        }

        [Theory, MemberData(nameof(NestedSinglesTestData.SinglesCanCalculate_NestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldCalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedData)
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<T>();

            var actual = calculateSingleFrequency.CalculateSingles(nestedData);

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        [Theory]
        [MemberData(nameof(NestedSinglesTestData.SinglesReturnEmpty_EmptyNestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldReturnEmptyWhenPassed_EmptyListsOrArrays<T>(IEnumerable<IEnumerable<T>> emptyData)
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<T>();

            var actual = calculateSingleFrequency.CalculateSingles(emptyData);

            Assert.Empty(actual);
        }

        [Fact]
        public void CalculateNestedSingles_ShouldThrowErrorWhenPassed_NullParentList()
        {
            var nullList = new List<List<int>> { null };
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<int>();

            Assert.Throws<System.ArgumentNullException>(() => calculateSingleFrequency.CalculateSingles(nullList));
        }
        //TODO: Find a way to check for null parents earlier.

        private IIndividualFrequency<T> CreateDefaultSinglesCalculator<T>()
        {
            return new CalculateIndividualFrequency<T>();
        }
    }
}
