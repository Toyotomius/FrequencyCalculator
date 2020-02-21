using FrequencyCalculator.DataModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    /// <summary>
    /// Class containing methods to calculate frequency in flat and nested lists
    /// (CalculateSingles & CalculateNestedSingles)
    /// </summary>
    /// <typeparam name="T">Requires base type (int, string, etc) during instantiation</typeparam>
    public static class CalculateIndividualFrequency
    {
        /// <summary>
        /// Calculates frequency of individual items from collections. Uses recursion for nested collections.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<Singles<T>> CalculateSingles<T>(this IEnumerable collection)
        {
            var list = new List<T>();
            var flattened = collection.Flatten();

            foreach (var itm in flattened)
            {
                if (itm is null)
                {
                    continue;
                }
                list.Add((T)itm);
            }

            var query = (from item in list
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
