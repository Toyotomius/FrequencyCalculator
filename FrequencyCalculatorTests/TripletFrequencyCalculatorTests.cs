using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class TripletFrequencyCalculatorShould
    {
        [Fact]
        public void CalculateWhenPassed_nestedListList()
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
        public void CalculateWhenPassed_NestedStringList()
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
        public void CalculateWhenPassed_NullList()
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
        public void CalculateWhenPassed_NullValues()
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
        public void ReturnCountOf_SpecifiedValuesInCollection()
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
        public void ThrowArgumentErrorWhenPassed_NullValueAsSpecifiedValue()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };

            var itemsToFind = new List<string> { "1", "2", null };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets(itemsToFind));
        }

        [Fact]
        public void ThrowArgumentErrorWhenNotPassed_ThreeDistinctElementsToFind()
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
        public void ThrowArgumentExceptionWhenPassed_LessThanThreeDistinctElements()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1, 2 } };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets());
        }
        [Fact]
        public void CalculateWhenPassed_SpecificCollectionOfTriplets()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 },
                                                   new List<int> { 1, 2, 3 }, new List<int> { 3, 4, 5 } };
            var itemsToFind = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 3, 4, 5 } };

            var expected = new List<Triplets<int>>
            {
                new Triplets<int> { Item = 1, Item2 = 2, Item3 = 3, Frequency = 3 },
                new Triplets<int> { Item = 3, Item2 = 4, Item3 = 5, Frequency = 1 }
            };

            var actual = nestedList.CalculateTriplets(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }
        [Fact]
        public void CalculateWhenMainCollectionHasNullsAndPassed_SpecificCollectionOfTriplets()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2, 3 }, null,
                                                   new List<int> { 1, 2, 3 }, new List<int> { 3, 4, 5 } };
            var itemsToFind = new int[][] { new int[] { 1, 2, 3 }, new int[] { 3, 4, 5 } };

            var expected = new List<Triplets<int>>
            {
                new Triplets<int> { Item = 1, Item2 = 2, Item3 = 3, Frequency = 2 },
                new Triplets<int> { Item = 3, Item2 = 4, Item3 = 5, Frequency = 1 }
            };

            var actual = nestedList.CalculateTriplets(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }
        [Fact]
        public void ThrowArgumentExceptionWhen_SubCollectionOfSpecificGroupHasNull()
        {
            var nestedList = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "1", "2", "3" },
                                                   new List<string> { "1", "2", "3" }, new List<string> { "3", "4", "5" } };
            var itemsToFind = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "3", "4", null } };

            Action act = () => { var result = nestedList.CalculateTriplets(itemsToFind); foreach (var itm in result) { } };
            Assert.Throws<ArgumentException>(act);
        }
    }
}
