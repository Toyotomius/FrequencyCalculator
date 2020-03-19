using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsGroupSpecifiedFrequency
    {
        /// <summary>
        /// Calculates frequency of a group of distinct pairs in nested collection. For custom types ensure you override
        /// GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T">Type of base element</typeparam>
        /// <param name="nestedCollection">Collection that contains the pair interested in</param>
        /// <param name="pair">Pair of elements to find the frequency of in the nested collection</param>
        /// <param name="IsSorted"> If true: uses Binary search algorithm. Every sub collection must be sorted </param>
        /// <returns>Empty if either pair element is null. Else returns a single Pair<T> object containing the pair
        /// searched and its frequency</returns>
        public static IEnumerable<Pairs<T>> CalculatePairs<T>(this IEnumerable<IList<T>> nestedCollection,
                                                                    IEnumerable<IList<T>> pairGroup, bool IsSorted = false) where T : IComparable<T>, IEquatable<T>
        {
            var index = 0;
            foreach (var pair in pairGroup)
            {
                if (pair is null) { index++; continue; }

                var distinct = pair.Distinct().Where(x => x is object);
                if (distinct.Count() != 2)
                {
                    throw new ArgumentException($"Group {index} of {nameof(pair)} in {nameof(pairGroup)} does not contain exactly two distinct values");
                }

                if (IsSorted)
                {
                    var frequency = SpecificCollectionSorted.CalculateFrequencyOfGroup(nestedCollection, pair);
                    yield return new Pairs<T> { Item = pair[0], Item2 = pair[1], Frequency = frequency };
                }

                else { yield return SpecificPairsUnSorted.CalculateSpecificPairsUnSorted(nestedCollection, pair); }


                index++;
            }
        }
    }
}
