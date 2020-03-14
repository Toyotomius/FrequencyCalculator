using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class SinglesSpecifiedFrequency
    {
        /// <summary>
        /// Calculates frequency of a distinct individual item from collections. Will use binary search algorithm if IsSorted is
        /// true. For custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T">
        /// Type of collection elements to be calculated. Must implement IComparable and IEquatable for custom types
        /// </typeparam>
        /// <param name="collection"> Collection to be searched. Must not be nested </param>
        /// <param name="value">      Value of the individual element you want the frequency of </param>
        /// <param name="IsSorted">  
        /// Default - false: Linear search for unsorted or nested collections.
        /// True: Binary search for flat sorted collections (much faster). (Note: True does not check for nested
        /// collections. If using a nested collection leave argument empty)
        /// </param>
        /// <returns> New Singles object with the value and number of occurrences </returns>
        public static Singles<T> CalculateSingles<T>(this IList<T> collection, T value,
                                                     bool IsSorted = false) where T : IComparable<T>, IEquatable<T>
        {
            if (IsSorted) { return SpecificSingleSorted.CalculateSinglesSorted(collection, value); }
            else { return SpecificSingleUnSorted.CalculateSinglesUnSorted(collection, value); }
        }

        /// <summary>
        /// Calculates frequency of a distinct individual item from collections. Uses recursion to flatten nested collections.
        /// For custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T">
        /// Type of collection elements to be calculated. Must implement IComparable and IEquatable for custom types
        /// </typeparam>
        /// <param name="collection"> Collection to be searched. Used for nested collections </param>
        /// <param name="value">      value of the individual element you want the frequency of </param>
        /// <returns> New Singles object with the value and number of occurrences </returns>
        public static Singles<T> CalculateSingles<T>(this IEnumerable<IList<T>> collection, T value)
                                                     where T : IComparable<T>, IEquatable<T>
        {
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
