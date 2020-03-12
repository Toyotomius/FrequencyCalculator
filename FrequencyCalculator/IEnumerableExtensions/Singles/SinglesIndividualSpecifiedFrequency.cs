using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class SinglesIndividualSpecifiedFrequency
    {
        /// <summary>
        /// Calculates frequency of an individual item from collections. Uses recursion to flatten nested collections.
        /// </summary>
        /// <typeparam name="T"> Type of collection elements to be calculated. Must implement IComparable </typeparam>
        /// <param name="collection"> Collection to be searched. Can be nested. </param>
        /// <param name="value">      Value of the individual element you want the frequency of </param>
        /// <param name="IsSorted">  
        /// False: Linear search for unsorted or nested collections.
        /// True: Binary search for flat sorted collections (much faster)
        /// </param>
        /// <returns> New Singles object with the value and number of occurrences </returns>
        public static Singles<T> CalculateSingles<T>(this IList<T> collection, T value, bool IsSorted) where T : IComparable<T>, IEquatable<T>
        {
            if (IsSorted) { return SinglesSorted.CalculateSinglesSorted(collection, value); }

            var flattened = collection.Flatten<T>();

            var count = 0;

            foreach (var itm in flattened)
            {
                if (itm.Equals(value))
                {
                    count++;
                }
            }

            return new Singles<T> { Item = value, Frequency = count };
        }
    }
}
