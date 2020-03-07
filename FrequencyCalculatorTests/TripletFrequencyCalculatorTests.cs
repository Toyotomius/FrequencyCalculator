using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;
using System;

namespace FrequencyCalculator.Tests
{
    public class TripletFrequencyCalculatorTests
    {
        [Fact]
        public void ShouldThrowArgumentExceptionWhenPassed_LessThanThreeDistinctElements()
        {
            var nestedList = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1, 2 } };



            Assert.Throws<ArgumentException>(() => nestedList.CalculateTriplets());
        }
    }
}
