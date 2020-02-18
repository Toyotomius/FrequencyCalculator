using FrequencyCalculator.Tests.TestData;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class CalculateIndividualSinglesTests
    {
        [Theory, MemberData(nameof(NestedSinglesTestData.SinglesCanCalculate_NestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldCalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedData)
        {
            var actual = nestedData.CalculateSingles<T>();

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        [Fact]
        public void CalculateNestedSingles_ShouldIgnoreNulls_NestedListWithNullLists()
        {
            var nullList = new List<List<int>> { null, new List<int> { 1, 1 } };

            var actual = nullList.CalculateSingles<int>();

            Assert.Contains(actual, x => x.Item == 1 && x.Frequency == 2);
        }

        [Fact]
        public void CalculateNestedSingles_ShouldIgnoreNulls_StringListWithNullValues()
        {
            var nullString = new List<string> { null, "1", "1" };

            var actual = nullString.CalculateSingles<string>();

            Assert.Contains(actual, x => x.Item == "1" && x.Frequency == 2);
        }

        [Theory]
        [MemberData(nameof(NestedSinglesTestData.SinglesReturnEmpty_EmptyNestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldReturnEmptyWhenPassed_EmptyListsOrArrays<T>(IEnumerable<IEnumerable<T>> emptyData)
        {
            var actual = emptyData.CalculateSingles<T>();

            Assert.Empty(actual);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateInstantiatedWith_ListOfStringLists()
        {
            var nestedStringList = new List<List<string>>() {
                                                                new List<string> {"0", "0" },
                                                                new List<string> {"0", "0" }
                                                            };

            var actual = nestedStringList.CalculateSingles<string>();

            Assert.Contains(actual, x => x.Item == "0" && x.Frequency == 4);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenInstatiatedWith_ListOfIntLists()
        {
            var nestedIntList = new List<List<int>>() {
                                                            new List<int> { 0, 0 },
                                                            new List<int> { 0, 0 }
                                                       };
            var actual = nestedIntList.CalculateSingles<int>();

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 4);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntArray()
        {
            var intArray = new int[] { 0, 0 };

            var actual = intArray.CalculateSingles<int>();

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntList()
        {
            var intList = new List<int>() { 0, 0 };

            var actual = intList.CalculateSingles<int>();

            Assert.Contains(actual, x => x.Item == 0 && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatStringList()
        {
            var stringList = new List<string> { "0", "0" };

            var actual = stringList.CalculateSingles<string>();

            Assert.Contains(actual, x => x.Item == "0" && x.Frequency == 2);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldReturnEmpty_NullLists()
        {
            var nullString = new List<string> { null };

            var actual = nullString.CalculateSingles<string>();

            Assert.Empty(actual);
        }

        //TODO: Find a way to check for null parents earlier.
    }
}
