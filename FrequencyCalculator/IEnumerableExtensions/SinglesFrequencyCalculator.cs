using FrequencyCalculator.DataModels;
using FrequencyCalculator.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    /// <summary> Class containing methods to calculate frequency in flat and nested lists (CalculateSingles &
    /// CalculateNestedSingles) </summary>

    public static class CalculateIndividualFrequency
    {
        /// <summary>
        /// Calculates frequency of individual items from collections. Uses recursion to flatten nested collections.
        /// </summary>
        /// <typeparam name="T"> Primitive type of collection elements to be searched </typeparam>
        /// <param name="collection"> </param>
        /// <returns> A List of Singles objects that each contain an element and its frequency </returns>
        public static List<Singles<T>> CalculateSingles<T>(this IEnumerable collection)
        {
            var flattened = collection.Flatten<T>();

            var query = (from item in flattened
                         group item by item into g
                         orderby g.Count() descending
                         select new Singles<T>
                         {
                             Item = g.Key,
                             Frequency = g.Count()
                         }).ToList();
            return query;
        }

        /// <summary>
        /// Calculates frequency of an individual item from collections. Uses recursion to flatten nested collections.
        /// </summary>
        /// <typeparam name="T"> Primitive type of collection elements to be searched </typeparam>
        /// <param name="collection"> Collection to be searched. Can be nested. </param>
        /// <param name="value">      Value of the individual element you want the frequency of </param>
        /// <param name="IsSorted">  
        /// False: Linear search for unsorted or nested collections.
        /// True: Binary search for flat sorted collections (much faster)
        /// </param>
        /// <returns> New Singles object with the value and number of occurrences </returns>
        public static Singles<T> CalculateSingles<T>(this IList<T> collection, T value, bool IsSorted) where T : IComparable
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

        /// <summary> 
        /// Calculates frequency of an individual item from collections. Uses recursion to flatten nested collections. 
        /// </summary> 
        /// <typeparam name="T"> Primitive type of collection elements to be searched </typeparam> 
        /// <param name="collection"> Collection to be searched. Can be nested. </param> 
        /// <param name="values"> Collection of values you want the frequency of </param> 
        /// <param name="IsSorted">
        /// False: Linear search for unsorted or nested collections.
        /// True: Binary search for flat sorted collections (much faster) 
        /// </param> 
        /// <returns> IEnumerable of Singles<T> object with the value and number of occurrences</returns>
        public static IEnumerable<Singles<T>> CalculateSingles<T>(this IList<T> collection, IList<T> values, bool IsSorted) where T : IComparable
        {
            foreach (var value in values)
            {
                yield return collection.CalculateSingles<T>(value, IsSorted);
            }
        }
    }
}
