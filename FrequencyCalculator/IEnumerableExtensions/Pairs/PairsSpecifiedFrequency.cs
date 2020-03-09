using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsSpecifiedFrequency
    {
        /// <summary> Calculates frequency of a specified distinct pair of elements. </summary>
        /// <typeparam name="T">Type of base element</typeparam>
        /// <param name="nestedCollection">Collection that contains the pair interested in</param>
        /// <param name="pair">Pair of elements to find the frequency of in the nested collection</param>
        /// <returns>Empty if either pair element is null. Else returns a single Pair<T> object containing the pair
        /// searched and its frequency</returns>
        public static Pairs<T> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection, IList<T> pair) where T : IComparable
        {
            var distinctPair = pair.Distinct();
            if (distinctPair.Count() != 2) { throw new ArgumentException($"{nameof(pair)} does not contain exactly two distinct elements"); }
            if (pair[0] is null || pair[1] is null)
            {
                return new Pairs<T> { };
            }

            return (from n in nestedCollection
                    where n != null
                    where n.Contains(pair[0]) && n.Contains(pair[1])
                    group n by pair into g
                    select new Pairs<T>
                    {
                        Item = pair[0],
                        Item2 = pair[1],
                        Frequency = g.Count()
                    }).First();
        }
    }
}
