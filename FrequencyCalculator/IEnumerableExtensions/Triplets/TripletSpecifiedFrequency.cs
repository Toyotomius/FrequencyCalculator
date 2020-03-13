using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class TripletSpecifiedFrequency
    {
        /// <summary> Calculates frequency of distinct tripets in nested collection. </summary>
        /// <typeparam name="T"> Type of base element in collections </typeparam>
        /// <param name="nestedCollection"> Nested collection to calculate triplet frequency from </param>
        /// <param name="triplet">         
        /// Collection of distinct triplets to find the frequency of in the nested collection
        /// </param>
        /// <returns> List of triplets objects in descending order of frequency </returns>
        public static Triplets<T> CalculateTriplets<T>(this IEnumerable<IEnumerable<T>> nestedCollection,
                                                       IList<T> triplet) where T : IComparable<T>, IEquatable<T>
        {
            var distinctTriplet = triplet.Distinct().Where(x => x != null);
            if (distinctTriplet.Count() != 3) { throw new ArgumentException($"{nameof(triplet)} does not contain exactly three distinct values"); }
            foreach (var num in triplet)
            {
                if (num is null) { return new Triplets<T> { }; }
            }

            return (from n in nestedCollection
                    where n != null
                    where n.Contains(triplet[0]) && n.Contains(triplet[1]) && n.Contains(triplet[2])
                    group n by triplet into g
                    orderby g.Count() descending
                    select new Triplets<T>
                    {
                        Item = triplet[0],
                        Item2 = triplet[1],
                        Item3 = triplet[2],
                        Frequency = g.Count()
                    }).First();
        }
    }
}
