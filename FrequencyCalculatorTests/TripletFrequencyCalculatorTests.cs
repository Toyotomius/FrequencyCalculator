using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class TripletFrequencyCalculatorTests
    {
        [Fact]
        public void ShouldCalculateWhenPassed_nestedListList()
        {
            var nestedList = new List<List<int>>
            {
                new List<int> { 1,2,3 },
                new List<int> { 1,2,3 }
            };
            var expected = new List<Triplets<int>> { new Triplets<int> { Item = 1, Item2 = 2, Item3 = 3, Frequency = 2 } };

            var actual = nestedList.CalculateTriplets();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldCalculateWhenPassed_NestedStringList()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2" ,"3" },
                new List<string> { "1","2" ,"3" }
            };
            var expected = new List<Triplets<string>> { new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 } };

            var actual = nestedList.CalculateTriplets();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldCalculateWhenPassed_NullList()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2" ,"3" },
                new List<string> { "1","2" ,"3" },
                null
            };
            var expected = new List<Triplets<string>> { new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 } };

            var actual = nestedList.CalculateTriplets();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldCalculateWhenPassed_NullValues()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2","3", null },
                new List<string> { "1","2","3", null }
            };
            var expected = new List<Triplets<string>> { new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 } };

            var actual = nestedList.CalculateTriplets();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldReturnCountOf_SpecifiedValuesInCollection()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", "2", "3" };
            var expected = new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 };

            var actual = nestedList.CalculateTriplets(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldReturnEmptyTripletsWhenPassed_NullValueAsSpecifiedValue()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", "2", null };
            var expected = new Triplets<string> { };

            var actual = nestedList.CalculateTriplets(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ShouldThrowArgumentErrorWhenNotPassed_ThreeDistinctElementsToFind()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };

            var itemsToFind = new List<string> { "1" };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets(itemsToFind));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenPassed_LessThanThreeDistinctElements()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1, 2 } };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets());
        }
    }
}
