using FrequencyCalculator.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    /// <summary>
    /// Class containing methods to calculate frequency in flat and nested lists
    /// (CalculateSingles & CalculateNestedSingles)
    /// </summary>
    /// <typeparam name="T">Requires type during instantiation</typeparam>
    public class CalculateIndividualFrequency<T> : IIndividualFrequency<T>
    {
        /// <summary>
        /// Calculates frequency of individual items from nested collections.
        /// </summary>
        /// <param name="nestedCollection"></param>
        /// <returns></returns>                
        public List<Singles<T>> CalculateNestedSingles(IEnumerable<IEnumerable<T>> nestedCollection)
        {
            if (nestedCollection.Any(x => x is null))
            {
                throw new System.ArgumentNullException($"{nestedCollection}", "Parent collection passed to CalculatedNestedSingles is null");
            }

            var query = (from number in nestedCollection.SelectMany(n => n)
                         group number by number into g
                         orderby g.Count() descending
                         select new Singles<T>
                         {
                             Item = g.Key,
                             Frequency = g.Count()
                         }).ToList();
            return query;
        }
        // TODO: Add recursion to allow for any amount of nesting.

        /// <summary>
        /// Calculates frequency of individual items from flat collections. 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public List<Singles<T>> CalculateSingles(IEnumerable<T> collection)
        {
            if (collection.IsNested())
            {
                throw new System.ArgumentException("Use CalculatedNestedSingles for nested collections where individual frequency is desired",
                    nameof(collection));
            }

            var query = (from item in collection
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
