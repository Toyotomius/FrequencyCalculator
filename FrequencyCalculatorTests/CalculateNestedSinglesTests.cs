using FrequencyCalculator.Tests.TestData;
using System.Collections.Generic;
using Xunit;

namespace FrequencyCalculator.Tests
{
    public class CalculateNestedSinglesTests
    {
        [Theory, MemberData(nameof(NestedSinglesTestData.NestedSinglesCanCalculate_Data), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldCalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedData)
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<T>();

            var actual = calculateSingleFrequency.CalculateNestedSingles(nestedData);

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        [Theory]
        [MemberData(nameof(NestedSinglesTestData.NestedSinglesReturnsEmpty_Data), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldReturnEmptyWhenPassed_EmptyListsOrArrays<T>(IEnumerable<IEnumerable<T>> emptyData)
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<T>();

            var actual = calculateSingleFrequency.CalculateNestedSingles(emptyData);

            Assert.Empty(actual);
        }

        [Fact]
        public void CalculateNestedSingles_ShouldThrowErrorWhenPassed_NullParentList()
        {
            var nullList = new List<List<int>> { null };
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<int>();

            Assert.Throws<System.ArgumentNullException>(() => calculateSingleFrequency.CalculateNestedSingles(nullList));
        }

        private IIndividualFrequency<T> CreateDefaultSinglesCalculator<T>()
        {
            return new CalculateIndividualFrequency<T>();
        }
    }
}
