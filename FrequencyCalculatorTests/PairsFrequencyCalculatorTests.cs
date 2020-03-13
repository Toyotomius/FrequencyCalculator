using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class PairsFrequencyCalculatorShould
    {
        #region CustomType

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullCustomTypeValue()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = null }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 1, Second = null }, new TestClass { First = 2, Second = 3 } },
            };
            var expected = new List<Pairs<TestClass>>
            {
                new Pairs<TestClass> { Item = new TestClass { First = 1, Second = null },
                                       Item2 = new TestClass{ First = 2, Second = 3 },
                                       Frequency = 2 }
            };

            var actual = valuesPassed.CalculatePairs<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullListElement()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                null
            };
            var expected = new List<Pairs<TestClass>>
            {
                new Pairs<TestClass> { Item = new TestClass { First = 1, Second = 2 },
                                       Item2 = new TestClass{ First = 2, Second = 3 },
                                       Frequency = 2 }
            };

            var actual = valuesPassed.CalculatePairs<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomSortedCollectionWhenPassed_SpecificValueToFind()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 4 } },
        };
            var valueToFind = new List<List<TestClass>> { new List<TestClass> { new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                                                          new List<TestClass> { new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 } } };

            var expected = new List<Pairs<TestClass>> { new Pairs<TestClass> { Item = new TestClass { First = 1, Second = 2 },
                                                                               Item2 = new TestClass { First = 2, Second = 3 },
                                                                               Frequency = 2 },
                                                        new Pairs<TestClass> { Item = new TestClass { First = 3, Second = 4 },
                                                                               Item2 = new TestClass { First = 2, Second = 3 },
                                                                               Frequency = 1 } };

            var actual = valuesPassed.CalculatePairs(valueToFind);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_CollectionOfCustomTypes()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 2, Second = 3 } },
            };
            var expected = new List<Pairs<TestClass>>
            {
                new Pairs<TestClass> { Item = new TestClass { First = 1, Second = 2 },
                                       Item2 = new TestClass{ First = 2, Second = 3 },
                                       Frequency = 2 }
            };

            var actual = valuesPassed.CalculatePairs<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ThrowArgumentExceptionWhenPassed_CustomTypeWithLessThanTwoDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, },
            };

            Assert.Throws<ArgumentException>(() => valuesPassed.CalculatePairs());
        }

        [Fact]
        public void ThrowArgumentExceptionWhenPassed_LessThanTwoDistinctElements()
        {
            var valuesPassed = new List<List<int>> { new List<int> { 1, 1 }, new List<int> { 1, 1 } };

            Assert.Throws<ArgumentException>(() => valuesPassed.CalculatePairs());
        }

        [Fact]
        public void ThrowArgumentExceptiuonWhenCustomTypePassed_GroupToFindWithLessThanTwoDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };
            var valueToFind = new List<List<TestClass>> {
                new List<TestClass> { new TestClass { First = 1, Second = 2 } }
            };

            // Due to deferred execution, have to run the foreach to force the exception.
            Action act = () => { var result = valuesPassed.CalculatePairs(valueToFind); foreach (var itm in result) { } };
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ThrowArgumentExceptiuonWhenCustomTypePassed_ItemsToFindWithLessThanTwoDistinct()
        {
            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },
            };
            var valueToFind = new List<TestClass> {
                new TestClass { First = 1, Second = 2 }
            };
            Assert.Throws<ArgumentException>(() => valuesPassed.CalculatePairs(valueToFind));
        }

        #endregion CustomType

        #region FindingSpecificValues

        [Fact]
        public void CalculateWhenMainCollectionHasNullsAndPassed_SpecificCollectionOfPairs()
        {
            var valuesPassed = new List<List<int>> { new List<int> { 1, 2 }, null,
                                                   new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            var itemsToFind = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } };

            var expected = new List<Pairs<int>>
            {
                new Pairs<int> { Item = 1, Item2 = 2, Frequency = 2 },
                new Pairs<int> { Item = 3, Item2 = 4, Frequency = 1 }
            };

            var actual = valuesPassed.CalculatePairs(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_SpecificCollectionOfPairs()
        {
            var valuesPassed = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1, 2 },
                                                   new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            var itemsToFind = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } };

            var expected = new List<Pairs<int>>
            {
                new Pairs<int> { Item = 1, Item2 = 2, Frequency = 3 },
                new Pairs<int> { Item = 3, Item2 = 4, Frequency = 1 }
            };

            var actual = valuesPassed.CalculatePairs(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ReturnCountOf_SpecifiedValuesInCollection()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", "2" };
            var expected = new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 };

            var actual = valuesPassed.CalculatePairs(itemsToFind);

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ThrowArgumentErrorWhenNotPassed_TwoDistinctElementsToFind()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" }
            };

            var itemsToFind = new List<string> { "1" };

            Assert.Throws<ArgumentException>(() => valuesPassed.CalculatePairs(itemsToFind));
        }

        [Fact]
        public void ThrowArgumentExceptionWhen_NullValueAsSpecifiedValue()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2", "3" },
                new List<string> { "1","2", "3" }
            };
            var itemsToFind = new List<string> { "1", null };

            Assert.Throws<ArgumentException>(() => valuesPassed.CalculatePairs(itemsToFind));
        }

        [Fact]
        public void ThrowArgumentExceptionWhen_SubCollectionOfSpecificGroupHasNull()
        {
            var valuesPassed = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "1", "2", "3" },
                                                   new List<string> { "1", "2", "3" }, new List<string> { "3", "4", "5" } };
            var itemsToFind = new List<List<string>> { new List<string> { "1", "2", "3" }, new List<string> { "3", "4", null } };

            // Due to deferred execution, have to run the foreach to force the exception.
            Action act = () => { var result = valuesPassed.CalculatePairs(itemsToFind); foreach (var itm in result) { } };
            Assert.Throws<ArgumentException>(act);
        }

        #endregion FindingSpecificValues

        [Fact]
        public void CalculateWhenPassed_NestedStringList()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" }
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = valuesPassed.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_NullList()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2" },
                new List<string> { "1","2" },
                null
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = valuesPassed.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_NullValues()
        {
            var valuesPassed = new List<List<string>>
            {
                new List<string> { "1","2", null },
                new List<string> { "1","2", null }
            };
            var expected = new List<Pairs<string>> { new Pairs<string> { Item = "1", Item2 = "2", Frequency = 2 } };

            var actual = valuesPassed.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_valuesPassedList()
        {
            var valuesPassed = new List<List<int>>
            {
                new List<int> { 1,2 },
                new List<int> { 1,2 }
            };
            var expected = new List<Pairs<int>> { new Pairs<int> { Item = 1, Item2 = 2, Frequency = 2 } };

            var actual = valuesPassed.CalculatePairs();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }
    }

    //TODO: Tests for nested Linklists, queues, etc
}
