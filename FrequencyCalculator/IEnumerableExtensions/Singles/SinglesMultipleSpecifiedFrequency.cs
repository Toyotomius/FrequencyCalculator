﻿using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class SinglesMultipleSpecifiedFrequency
    {
        /// <summary> 
        /// Calculates frequency of an individual item from collections. Uses recursion to flatten nested collections. 
        /// For custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T"> Type of collection elements to be calculated. Must implement IComparable </typeparam> 
        /// <param name="collection"> Collection to be searched. Can be nested. </param> 
        /// <param name="values"> Collection of values you want the frequency of. Ignores null elements </param> 
        /// <param name="IsSorted">
        /// False: Linear search for unsorted or nested collections.
        /// True: Binary search for flat sorted collections (much faster) 
        /// </param> 
        /// <returns> IEnumerable of Singles<T> object with the value and number of occurrences</returns>
        public static IEnumerable<Singles<T>> CalculateSingles<T>(this IList<T> collection, IList<T> values,
                                                                  bool IsSorted) where T : IComparable<T>, IEquatable<T>
        {
            foreach (var value in values)
            {
                if(value is null) { continue; }
                yield return collection.CalculateSingles(value, IsSorted);
            }
        }
    }
}
