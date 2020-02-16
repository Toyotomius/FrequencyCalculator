﻿using FrequencyCalculator.DataModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    /// <summary>
    /// Class containing methods to calculate frequency in flat and nested lists
    /// (CalculateSingles & CalculateNestedSingles)
    /// </summary>
    /// <typeparam name="T">Requires base type (int, string, etc) during instantiation</typeparam>
    public class CalculateIndividualFrequency<T> : IIndividualFrequency<T>
    {
        /// <summary>
        /// Calculates frequency of individual items from collections. Uses recursion for nested collections.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public List<Singles<T>> CalculateSingles(IEnumerable collection)
        {
            
           
            var list = new List<T>();
            var flattened = collection.Flatten();

            foreach(var itm in flattened)
            {
                if (itm is null)
                {
                    throw new System.ArgumentNullException($"{collection}", $"Parent collection passed to {nameof(CalculateSingles)} is null");
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
