using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class TripletGroupSpecifiedFrequency
    {
        /// <summary>
        /// Calculates frequency of a group of distinct tripets in nested collection using deferred execution. For
        /// custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T"> Type of base element in collections </typeparam>
        /// <param name="nestedCollection"> Nested collection to calculate triplet frequency from </param>
        /// <param name="tripletGroup">    
        /// Collection of sub collections of distinct triplets to find the frequency of in the nested collection. Null
        /// in place of sub collection are ignored.
        /// </param>
        /// <returns> IEnumerable of triplets objects in descending order of frequency </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when a sub collection does not contain exactly three distinct values. Includes nulls in sub collection.
        /// </exception>
        public static IEnumerable<Triplets<T>> CalculateTriplets<T>(this IEnumerable<IList<T>> nestedCollection,
                                                                    IEnumerable<IList<T>> tripletGroup,
                                                                    bool IsSorted = false) where T : IComparable<T>, IEquatable<T>
        {
            var index = 0;
            foreach (var triplet in tripletGroup)
            {
                if (triplet is null) { index++; continue; }

                var distinct = triplet.Distinct().Where(x => x is object);
                if (distinct.Count() != 3)
                {
                    throw new ArgumentException($"Group {index} of {nameof(triplet)} in {nameof(tripletGroup)} does not contain exactly three distinct values");
                }
                if (IsSorted)
                {
                    var frequency = SpecificCollectionSorted.CalculateFrequencyOfGroup(nestedCollection, triplet);
                    yield return new Triplets<T> { Item = triplet[0], Item2 = triplet[1], Item3 = triplet[2], Frequency = frequency };
                }
                else { yield return SpecificTripletsUnSorted.CalculateSpecifictripletsUnSorted(nestedCollection, triplet); }

                index++;
            }
        }
    }
}
