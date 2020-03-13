using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.Helpers
{
    internal static class SpecificTripletsUnSorted
    {
        /// <summary> Finds frequency of a distinct specific triplet of elements in an unsorted nested collection. </summary>
        /// <typeparam name="T"> Type of base element </typeparam>
        /// <param name="nestedCollection"> Collection of collections to iterate through to find distinct triplet frequency </param>
        /// <param name="triplet">             triplet of elements to find the frequency of </param>
        /// <returns> New triplet object of the triplet to find and their frequency </returns>
        internal static Triplets<T> CalculateSpecifictripletsUnSorted<T>(IEnumerable<IList<T>> nestedCollection,
                                                         IList<T> triplet) where T : IComparable<T>, IEquatable<T>
        {
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
