using System.Collections.Generic;
using Xunit;
using FrequencyCalculator.Tests.TestData;

namespace FrequencyCalculator.Tests
{
    public class CalculateNestedSinglesTests
    {


        [Theory, MemberData(nameof(NestedSinglesTestData.NestedSinglesData), MemberType = typeof(NestedSinglesTestData))]
        public void CalculateNestedSingles_ShouldCalculateWhenPassed_NestedListsOrArrays<T>(IEnumerable<IEnumerable<T>> nestedList)
        {
            var calculateSingleFrequency = CreateDefaultSinglesCalculator<T>();

            var actual = calculateSingleFrequency.CalculateNestedSingles(nestedList);

            Assert.Contains(actual, x => x.Frequency == 4);
        }

        private IIndividualFrequency<T> CreateDefaultSinglesCalculator<T>()
        {
            return new CalculateIndividualFrequency<T>();
        }
    }
}
