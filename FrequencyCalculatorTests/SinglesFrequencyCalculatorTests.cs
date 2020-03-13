using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using FrequencyCalculator.Tests.TestData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public partial class SinglesFrequencyCalculatorShould
    {
        #region Theory

        [Theory, MemberData(nameof(NestedSinglesTestData.SinglesCanCalculate_NestedData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedData) where T : IComparable<T>, IEquatable<T>
        {
            var actual = nestedData.CalculateSingles<T>();

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        [Theory]
        [MemberData(nameof(NestedSinglesTestData.SinglesReturnEmpty_EmptyNestedData), MemberType = typeof(NestedSinglesTestData))]
        public void ReturnEmptyWhenPassed_EmptyListsOrArrays<T>(IEnumerable<IEnumerable<T>> emptyData) where T : IComparable<T>, IEquatable<T>
        {
            var actual = emptyData.CalculateSingles<T>();

            Assert.Empty(actual);
        }

        #endregion Theory

        #region CustomType

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullCustomTypeValue()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 3, Second = 4 },
                new TestClass { First = 1, Second = null },
                new TestClass { First = 1, Second = null }
            };

            var expected = new List<Singles<TestClass>>
            {
                new Singles<TestClass> { Item = new TestClass { First = 1, Second = null }, Frequency = 2 },
                new Singles<TestClass> { Item = new TestClass { First = 3, Second = 4 }, Frequency = 1 }
            };

            var actual = valuesPassed.CalculateSingles<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomCollectionWhenPassed_NullListElement()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 1 },
                null
            };
            var expected = new List<Singles<TestClass>>
            {
                new Singles<TestClass> { Item = new TestClass { First = 1, Second = 2 }, Frequency = 2 },
                new Singles<TestClass> { Item = new TestClass { First = 1, Second = 1 }, Frequency = 1 }
            };

            var actual = valuesPassed.CalculateSingles<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomSortedCollectionWhenPassed_SpecificGroupToFind()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 3, Second = 4 },
                new TestClass { First = 5, Second = 6 }
            };
            var valueToFind = new List<TestClass> { new TestClass { First = 1, Second = 2 },
                                                     new TestClass{First = 3, Second = 4 } };

            var expected = new List<Singles<TestClass>> { new Singles<TestClass> { Item = new TestClass { First = 1, Second = 2 }, Frequency = 2 },
                                                           new Singles<TestClass> { Item = new TestClass { First = 3, Second = 4 }, Frequency = 1 } };

            var actual = valuesPassed.CalculateSingles(valueToFind, true);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomSortedCollectionWhenPassed_SpecificValueToFind()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 3, Second = 4 },
                new TestClass { First = 5, Second = 6 }
            };
            var valueToFind = new TestClass { First = 1, Second = 2 };

            var expected = new Singles<TestClass> { Item = new TestClass { First = 1, Second = 2 }, Frequency = 2 };

            var actual = valuesPassed.CalculateSingles(valueToFind, true);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateCustomUnsortedCollectionWhenPassed_SpecificValueToFind()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 3, Second = 4 },
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 5, Second = 6 },
                new TestClass { First = 1, Second = 2 }
            };
            var valueToFind = new TestClass { First = 1, Second = 2 };

            var expected = new Singles<TestClass> { Item = new TestClass { First = 1, Second = 2 }, Frequency = 2 };

            var actual = valuesPassed.CalculateSingles(valueToFind, false);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_CollectionOfCustomTypes()
        {
            var valuesPassed = new List<TestClass>
            {
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 2 },
                new TestClass { First = 1, Second = 1 }
            };
            var expected = new List<Singles<TestClass>>
            {
                new Singles<TestClass> { Item = new TestClass { First = 1, Second = 2 }, Frequency = 2 },
                new Singles<TestClass> { Item = new TestClass { First = 1, Second = 1 }, Frequency = 1 }
            };

            var actual = valuesPassed.CalculateSingles<TestClass>();
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        #endregion CustomType

        #region FindingSpecificValues

        [Fact]
        public void ReturnCountOf_SpecifiedValueInSortedCollection()
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
        public void ReturnCountOf_SpecifiedValueInUnSortedCollection()
        {
            var valuePassed = "1";
            var stringList = new List<string> { "1", "3", "2", "1" };

            var expected = new Singles<string> { Item = "1", Frequency = 2 };

            var actual = stringList.CalculateSingles<string>(valuePassed, false);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ReturnIEnumerableofSinglesWhenPassed_CollectionToCalculate()
        {
            var valuesPassed = new List<string> { "1", "2" };
            var stringList = new List<string> { "1", "3", "2", "1" };

            var expected = new List<Singles<string>>{ new Singles<string> { Item = "1", Frequency = 2 },
                                                      new Singles<string> { Item = "2", Frequency = 1 }};

            var actual = stringList.CalculateSingles<string>(valuesPassed, false);
            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        #endregion FindingSpecificValues

        [Fact]
        public void CalculateInstantiatedWith_ListOfStringLists()
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
        public void CalculateWhenInstatiatedWith_ListOfIntLists()
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
        public void CalculateWhenPassed_FlatIntArray()
        {
            var intArray = new int[] { 0, 0 };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 0, Frequency = 2 } };

            var actual = intArray.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_FlatIntList()
        {
            var intList = new List<int>() { 0, 0 };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 0, Frequency = 2 } };

            var actual = intList.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_FlatStringList()
        {
            var stringList = new List<string> { "0", "0" };
            var expected = new List<Singles<string>> { new Singles<string> { Item = "0", Frequency = 2 } };

            var actual = stringList.CalculateSingles<string>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void CalculateWhenPassed_LinkedList()
        {
            var ll = new LinkedList<int>();
            ll.AddLast(1);
            ll.AddLast(1);

            var expected = new List<Singles<int>> { new Singles<int> { Item = 1, Frequency = 2 } };

            var actual = ll.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void IgnoreNulls_NestedListWithNullLists()
        {
            var nullList = new List<List<int>> { null, new List<int> { 1, 1 } };
            var expected = new List<Singles<int>> { new Singles<int> { Item = 1, Frequency = 2 } };

            var actual = nullList.CalculateSingles<int>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void IgnoreNulls_StringListWithNullValues()
        {
            var nullString = new List<string> { null, "1", "1" };
            var expected = new List<Singles<string>> { new Singles<string> { Item = "1", Frequency = 2 } };

            var actual = nullString.CalculateSingles<string>();

            var actualStr = JsonConvert.SerializeObject(actual);
            var expectedStr = JsonConvert.SerializeObject(expected);

            Assert.Equal(expectedStr, actualStr);
        }

        [Fact]
        public void ReturnEmpty_NullLists()
        {
            var nullString = new List<string> { null };

            var actual = nullString.CalculateSingles<string>();

            Assert.Empty(actual);
        }
    }
}
