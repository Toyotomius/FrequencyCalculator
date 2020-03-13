using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsFrequencyCalculator
    {
        /// <summary>
        /// Calculates all distinct pairs from a nested collection and their frequencies. For custom types ensure you
        /// override GetHashCode when implementing IEquatable for correct grouping.
        /// </summary>
        /// <typeparam name="T"> Type of the base element that must implement IComparable </typeparam>
        /// <param name="nestedCollection"> </param>
        /// <returns> List of Pairs object sorted in descending order of frequency </returns>
        public static List<Pairs<T>> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection) where T : IComparable<T>, IEquatable<T>
        {
            var distinct = nestedCollection.Where(x => x is object).SelectMany(x => x).Where(x => x is object).Distinct().ToArray();

            var pairs = (from firstNum in distinct
                         from secondNum in distinct
                         where firstNum.CompareTo(secondNum) < 0
                         select new
                         {
                             First = firstNum,
                             Second = secondNum
                         }).ToArray();

            if (0 == pairs.Length)
            {
                throw new ArgumentException("Nested collection passed does not contain the at least two distinct elements to find pairs of");
            }

            return (from n in nestedCollection
                    where n != null
                    from p in pairs
                    where n.Contains(p.First) && n.Contains(p.Second)
                    group n by p into g
                    orderby g.Count() descending
                    select new Pairs<T>
                    {
                        Item = g.Key.First,
                        Item2 = g.Key.Second,
                        Frequency = g.Count()
                    }).ToList();
        }
    }
}
