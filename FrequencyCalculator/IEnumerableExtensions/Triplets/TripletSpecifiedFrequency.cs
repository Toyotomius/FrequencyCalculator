using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class TripletSpecifiedFrequency
    {
        /// <summary> Calculates frequency of distinct tripets in nested collection. 
        /// For custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T"> Type of base element in collections </typeparam>
        /// <param name="nestedCollection"> Nested collection to calculate triplet frequency from </param>
        /// <param name="triplet">         
        /// Collection of distinct triplets to find the frequency of in the nested collection
        /// </param>
        /// <returns> Triplet object containing the triplet and its frequency </returns>
        public static Triplets<T> CalculateTriplets<T>(this IEnumerable<IList<T>> nestedCollection,
                                                       IList<T> triplet, bool IsSorted = false) where T : IComparable<T>, IEquatable<T>
        {
            var distinctTriplet = triplet.Distinct().Where(x => x != null);
            if (distinctTriplet.Count() != 3) { throw new ArgumentException($"{nameof(triplet)} does not contain exactly three distinct values"); }

            if (IsSorted)
            {
                var frequency = SpecificCollectionSorted.CalculateFrequencyOfGroup(nestedCollection, triplet);
                return new Triplets<T> { Item = triplet[0], Item2 = triplet[1], Item3 = triplet[2], Frequency = frequency };
            }

            else { return SpecificTripletsUnSorted.CalculateSpecifictripletsUnSorted(nestedCollection, triplet); }
        }
    }
}
