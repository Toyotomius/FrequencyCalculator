using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using FrequencyCalculator.Tests.TestData;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class SinglesFrequencyCalculatorTests
    {
        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateInstantiatedWith_ListOfStringLists()
        {
            var nestedStringList = new List<List<string>>() {
                                                                new List<string> {"0", "0" },
                                                                new List<string> {"1", "1" }
                                                            };
            var expected = new List<Singles<string>> { new Singles<string> { Item = "0", Frequency = 2 },
                                                       new Singles<string> { Item = "1", Frequency = 2 } };

            var actual = nestedStringList.CalculateSingles<string>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenInstatiatedWith_ListOfIntLists()
        {
            var nestedIntList = new List<List<int>>() {
                                                            new List<int> { 0, 0 },
                                                            new List<int> { 0, 0 }
                                                       };

            var expected = new List<Singles<int>> { new Singles<int> { Item = 0, Frequency = 4 } };

            var actual = nestedIntList.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntArray()
        {
            var intArray = new int[] { 0, 0 };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 0, Frequency = 2 } };

            var actual = intArray.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatIntList()
        {
            var intList = new List<int>() { 0, 0 };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 0, Frequency = 2 } };

            var actual = intList.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_FlatStringList()
        {
            var stringList = new List<string> { "0", "0" };
            var expected = new List<Singles<string>> { new Singles<string> { Item = "0", Frequency = 2 } };

            var actual = stringList.CalculateSingles<string>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Theory, MemberData(nameof(NestedSinglesTestData.SinglesCanCalculate_NestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateSinglesFrequency_ShouldCalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedData)
        {
            var actual = nestedData.CalculateSingles<T>();

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldIgnoreNulls_NestedListWithNullLists()
        {
            var nullList = new List<List<int>> { null, new List<int> { 1, 1 } };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 1, Frequency = 2 } };

            var actual = nullList.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldIgnoreNulls_StringListWithNullValues()
        {
            var nullString = new List<string> { null, "1", "1" };
            var expected = new List<Singles<string>> { new Singles<string> { Item = "1", Frequency = 2 } };

            var actual = nullString.CalculateSingles<string>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldReturnEmpty_NullLists()
        {
            var nullString = new List<string> { null };

            var actual = nullString.CalculateSingles<string>();

            Assert.Empty(actual);
        }

        [Theory]
        [MemberData(nameof(NestedSinglesTestData.SinglesReturnEmpty_EmptyNestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateSinglesFrequency_ShouldReturnEmptyWhenPassed_EmptyListsOrArrays<T>(IEnumerable<IEnumerable<T>> emptyData)
        {
            var actual = emptyData.CalculateSingles<T>();

            Assert.Empty(actual);
        }
        [Fact]
        public void CalculateSinglesFrequency_ShouldReturnCountOf_SpecifiedValueInSortedCollection()
        {
            var valuePassed = "1";
            var stringList = new List<string> { "1", "1", "2", "3" };

            var expected = new Singles<string> { Item = "1", Frequency = 2 };

            var actual = stringList.CalculateSingles<string>(valuePassed, true);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateSinglesFrequency_ShouldReturnCountOf_SpecifiedValueInUnSortedCollection()
        {
            var valuePassed = "1";
            var stringList = new List<string> { "1", "3", "2", "1" };

            var expected = new Singles<string> { Item = "1", Frequency = 2 };

            var actual = stringList.CalculateSingles<string>(valuePassed, false);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        //TODO: Find a way to check for null parents earlier.
    }
}
