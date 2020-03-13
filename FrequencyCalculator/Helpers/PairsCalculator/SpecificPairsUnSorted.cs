using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.Helpers
{
    internal static class SpecificPairsUnSorted
    {
        /// <summary> Finds frequency of a distinct specific pair of elements in an unsorted nested collection. </summary>
        /// <typeparam name="T"> Type of base element </typeparam>
        /// <param name="nestedCollection"> Collection of collections to iterate through to find distinct pair frequency </param>
        /// <param name="pair">             Pair of elements to find the frequency of </param>
        /// <returns> New Pair object of the pair to find and their frequency </returns>
        internal static Pairs<T> CalculateSpecificPairsUnSorted<T>(IEnumerable<IList<T>> nestedCollection,
                                                         IList<T> pair) where T : IComparable<T>, IEquatable<T>
        {
            return (from n in nestedCollection
                    where n != null
                    where n.Contains(pair[0]) && n.Contains(pair[1])
                    group n by pair into g
                    orderby g.Count() descending
                    select new Pairs<T>
                    {
                        Item = pair[0],
                        Item2 = pair[1],
                        Frequency = g.Count()
                    }).First();
        }
    }
}
