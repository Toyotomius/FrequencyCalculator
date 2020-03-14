using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsSpecifiedFrequency
    {
        /// <summary>
        /// Calculates frequency of distinct pairs in nested collection. For custom types ensure you override
        /// GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T">Type of base element</typeparam>
        /// <param name="nestedCollection">Collection that contains the pair interested in</param>
        /// <param name="pair">Pair of elements to find the frequency of in the nested collection</param>
        /// <param name="IsSorted"> If true: uses Binary search algorithm. Every sub collection must be sorted </param>
        /// <returns>Empty if either pair element is null. Else returns a single Pair<T> object containing the pair
        /// searched and its frequency</returns>
        public static Pairs<T> CalculatePairs<T>(this IEnumerable<IList<T>> nestedCollection,
                                                 IList<T> pair, bool IsSorted = false) where T : IComparable<T>, IEquatable<T>
        {
            var distinctPair = pair.Distinct().Where(x => x != null);
            if (distinctPair.Count() != 2) { throw new ArgumentException($"{nameof(pair)} does not contain exactly two distinct elements"); }

            if (IsSorted) 
            { 
                var frequency = SpecificCollectionSorted.CalculateFrequencyOfGroup(nestedCollection, pair);
                return new Pairs<T> { Item = pair[0], Item2 = pair[1], Frequency = frequency };
            }

            else { return SpecificPairsUnSorted.CalculateSpecificPairsUnSorted(nestedCollection, pair); }

            
        }
    }
}
