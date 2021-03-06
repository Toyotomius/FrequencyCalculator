﻿using FrequencyCalculator.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    /// <summary> Class containing methods to calculate distinct individual item frequency in flat and nested lists </summary>

    public static class CalculateIndividualFrequency
    {
        /// <summary>
        /// Calculates frequency of distinct individual items from collections. Uses recursion to flatten nested collections. For
        /// custom types ensure you override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T"> Type of collection elements to be calculated. Must implement IComparable </typeparam>
        /// <param name="collection"> </param>
        /// <returns> A List of Singles objects that each contain an element and its frequency </returns>
        public static List<Singles<T>> CalculateSingles<T>(this IEnumerable collection) where T : IComparable<T>, IEquatable<T>
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
    }
}
