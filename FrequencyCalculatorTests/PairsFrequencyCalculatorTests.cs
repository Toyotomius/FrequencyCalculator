﻿using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class PairsFrequencyCalculatorShould
    {
        [Fact]
        public void CalculateWhenPassed_NestedIntList()
        {
            var nestedInt = new List<List<int>>
            {
                new List<int> { 1,2 },
                new List<int> { 1,2 }
            };
            var expected = new List<Pairs<int>> { new Pairs<int> { Item = 1, Item2 = 2, Frequency = 2 } };

            var actual = nestedInt.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_NestedStringList()
        {
            var nestedInt = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" }
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = nestedInt.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_NullList()
        {
            var nestedInt = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" },
                null
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = nestedInt.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_NullValues()
        {
            var nestedInt = new List<List<string>>
            {
                new List<string> { "1","2", null },
                new List<string> { "1","2", null }
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = nestedInt.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ReturnCountOf_SpecifiedValuesInCollection()
        {
            var nestedInt = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", "2" };
            var expected = new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 };

            var actual = nestedInt.CalculatePairs(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ReturnEmptyPairsWhenPassed_NullValueAsSpecifiedValue()
        {
            var nestedInt = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", null };
            var expected = new Pairs<string> { };

            var actual = nestedInt.CalculatePairs(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ThrowArgumentErrorWhenNotPassed_ThreeDistinctElementsToFind()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" }
            };

            var itemsToFind = new List<string> { "1" };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets(itemsToFind));
        }

        [Fact]
        public void ThrowArgumentExceptionWhenPassed_LessThanTwoDistinctElements()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 1 }, new List<int> { 1, 1 } };

            Assert.Throws<ArgumentException>(() => nestedList.CalculatePairs());
        }

        //TODO: Tests for nested Linklists, queues, etc
    }
}
