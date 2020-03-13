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
        #region CustomTypes

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullCustomTypeValue()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = null }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = null }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 } },
            };
            var expected = new List<Triplets<TestClass>>
            {
                new Triplets<TestClass> { Item = new TestClass { First = 1, Second = null },
                                          Item2 = new TestClass{ First = 3, Second = 4 },
                                          Item3 = new TestClass { First = 5, Second = 6 },
                                          Frequency = 2 }
            };

            var actual = valuesPassed.CalculateTriplets<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullListElement()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                                     null,
            };
            var expected = new List<Triplets<TestClass>>
            {
                new Triplets<TestClass> { Item = new TestClass  { First = 1, Second = 2 },
                                          Item2 = new TestClass { First = 3, Second = 4 },
                                          Item3 = new TestClass { First = 5, Second = 6 },
                                          Frequency = 2 }
            };

            var actual = valuesPassed.CalculateTriplets<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomSortedCollectionWhenPassed_SpecificValueToFind()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };
            var valueToFind = new List<List<TestClass>> {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };

            var expected = new List<Triplets<TestClass>> { new Triplets<TestClass> { Item  = new TestClass { First = 1, Second = 2 },
                                                                                     Item2 = new TestClass { First = 3, Second = 4 },
                                                                                     Item3 = new TestClass { First = 5, Second = 6 },
                                                                                     Frequency = 2 },
                                                           new Triplets<TestClass> { Item  = new TestClass { First = 3, Second = 4 },
                                                                                     Item2 = new TestClass { First = 2, Second = 3 },
                                                                                     Item3 = new TestClass { First = 2, Second = 4 },
                                                                                     Frequency = 1 } };

            var actual = valuesPassed.CalculateTriplets(valueToFind);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_CollectionOfCustomTypes()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 } },
            };
            var expected = new List<Triplets<TestClass>>
            {
                new Triplets<TestClass> { Item = new TestClass  { First = 1, Second = 2 },
                                          Item2 = new TestClass { First = 3, Second = 4 },
                                          Item3 = new TestClass { First = 5, Second = 6 },
                                          Frequency = 2 }
            };

            var actual = valuesPassed.CalculateTriplets<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ThrowArgumentExceptionWhenPassed_CustomTypeWithLessThanThreeDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }  },
            };

            Assert.Throws<ArgumentException>(() => valuesPassed.CalculateTriplets());
        }

        [Fact]
        public void ThrowArgumentExceptiuonWhenCustomTypePassed_GroupToFindWithLessThanThreeDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };
            var valueToFind = new List<List<TestClass>> {
                new List<TestClass> { new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4  } }
            };

            // Due to deferred execution, have to run the foreach to force the exception.
            Action act = () => { var result = valuesPassed.CalculateTriplets(valueToFind); foreach (var itm in result) { } };
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ThrowArgumentExceptiuonWhenCustomTypePassed_ItemsToFindWithLessThanThreeDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };
            var valueToFind = new List<TestClass> {
                new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4  }
            };
            Assert.Throws<ArgumentException>(() => valuesPassed.CalculateTriplets(valueToFind));
        }

        #endregion CustomTypes

        #region FindingSpecificValues

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
        public void ThrowArgumentExceptionWhen_SubCollectionOfSpecificGroupHasNull()
        {
            var nestedList = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "1", "2", "3" },
                                                      new List<string> { "1", "2", "3" }, new List<string> { "3", "4", "5" } };

            var itemsToFind = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "3", "4", null } };

            // Due to deferred execution, have to run the foreach to force the exception.
            Action act = () => { var result = nestedList.CalculateTriplets(itemsToFind); foreach (var itm in result) { } };
            Assert.Throws<ArgumentException>(act);
        }
        [Fact]
        public void CalculateFrequencyOfSpecifiedItemsWhen_IsSortedIsTrue()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1", "2", "3" },
                new List<string> { "1", "2", "3" }
            };
            var itemsToFind = new List<string> { "1", "2", "3" };
            var expected = new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 };

            var actual = nestedList.CalculateTriplets(itemsToFind, true);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }
        [Fact]
        public void CalculateFrequencyOfGroupOfSpecifiedItemsWhen_IsSortedIsTrue()
        {
            var nestedList = new List<List<string>>
            {
                new List<string> { "1", "2", "3" },
                new List<string> { "1", "2", "3" },
                new List<string> { "1", "2", "4" },
                new List<string> { "1", "2", "4" }
            };
            var itemsToFind = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "1", "2", "4" } };
            var expected = new List<Triplets<string>> 
            {
               new Triplets<string> { Item = "1", Item2 = "2", Item3 = "3", Frequency = 2 },
               new Triplets<string> { Item = "1", Item2 = "2", Item3 = "4", Frequency = 2 }
            };

            var actual = nestedList.CalculateTriplets(itemsToFind, true);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }
        #endregion FindingSpecificValues

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
        public void ThrowArgumentExceptionWhenPassed_LessThanThreeDistinctElements()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1, 2 } };

            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets());
        }
    }
}
