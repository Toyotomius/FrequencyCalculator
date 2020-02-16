using System.Collections.Generic;

namespace FrequencyCalculator.Tests.TestData
{
    public static class NestedSinglesTestData
    {
        public static IEnumerable<object[]> SinglesCanCalculate_NestedData()
        {
            yield return new object[]
            {
                new List<List<int>>()
                {
                    new List<int> { 0, 0 },
                    new List<int> { 0, 0 }
                }
            };
            yield return new object[]
            {
                new List<List<string>>()
                {
                    new List<string> { "0", "0" },
                    new List<string> { "0", "0" }
                }
            };
            yield return new object[]
            {
                new List<List<double>>()
                {
                    new List<double> { 0.1, 0.1 },
                    new List<double> { 0.1, 0.1 }
                }
            };
            yield return new object[]
            {
                new List<int[]>()
                {
                    new int[] { 0, 0 },
                    new int[] { 0, 0 }
                }
            };
            yield return new object[]
            {
                new List<string[]>()
                {
                    new string[] { "0", "0" },
                    new string[] { "0", "0" }
                }
            };
            yield return new object[]
            {
                new List<double[]>()
                {
                    new double[] { 0.1, 0.1 },
                    new double[] { 0.1, 0.1}
                }
            };
            yield return new object[]
            {
                new int[][]
                {
                    new int[] { 0, 0 },
                    new int[] { 0, 0 }
                }
            };
            yield return new object[]
            {
                new double[][]
                {
                    new double[] { 0.1, 0.1 },
                    new double[] { 0.1, 0.1 }
                }
            };
            yield return new object[]
            {
                new string[][]
                {
                    new string[] { "a", "a" },
                    new string[] { "a", "a" }
                }
            };
        }

        public static IEnumerable<object[]> SinglesReturnEmpty_EmptyNestedData()
        {
            yield return new object[]
            {
                new List<List<int>>
                {
                    new List<int>(),
                    new List<int>()
                }
            };
        }
    }
}
