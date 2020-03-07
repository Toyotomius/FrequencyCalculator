using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsFrequencyCalculator
    {
        /// <summary>
        /// Calculates all distinct pairs from a nested collection and their frequencies
        /// </summary>
        /// <typeparam name="T"> Type of the base element that must implement IComparable </typeparam>
        /// <param name="nestedCollection"> </param>
        /// <returns> List of Pairs object sorted in descending order of frequency </returns>
        public static List<Pairs<T>> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection) where T : IComparable
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

            if(0 == pairs.Length)
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

        /// <summary> Calculates frequency of a specified distinct pair of elements. </summary> 
        /// <typeparam name="T">Type of base element</typeparam> 
        /// <param name="nestedCollection">Collection that contains the pair interested in</param> 
        /// <param name="pair">Pair of elements to find the frequency of in the nested collection</param>
        /// <returns>Empty if either pair element is null. Else returns a single Pair<T> object containing the pair
        /// searched and its frequency</returns>
        public static Pairs<T> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection, IList<T> pair) where T : IComparable
        {
            if (pair[0] is null || pair[1] is null)
            {
                return new Pairs<T> { };
            }

            return (from n in nestedCollection
                    where n != null
                    where n.Contains(pair[0]) && n.Contains(pair[1])
                    group n by pair into g
                    select new Pairs<T>
                    {
                        Item = pair[0],
                        Item2 = pair[1],
                        Frequency = g.Count()
                    }).First();
        }
    }
}
