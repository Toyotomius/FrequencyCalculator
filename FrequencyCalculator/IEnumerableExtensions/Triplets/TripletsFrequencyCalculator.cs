using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class TripletsFrequencyCalculator
    {
        /// <summary> Calculates frequency of distinct tripets in nested collection. </summary>
        /// <typeparam name="T"> Type of base element in collections </typeparam>
        /// <param name="nestedCollection"> Nested collection to calculate triplet frequency from </param>
        /// <returns> List of triplets objects in descending order of frequency </returns>
        public static List<Triplets<T>> CalculateTriplets<T>(this IEnumerable<IEnumerable<T>> nestedCollection) where T : IComparable<T>, IEquatable<T>
        {
            var distinct = nestedCollection.Where(x => x is object).SelectMany(x => x).Where(x => x is object).Distinct().ToArray();

            var triplets = (from firstNum in distinct
                            from secondNum in distinct
                            from thirdNum in distinct
                            where firstNum.CompareTo(secondNum) < 0 && secondNum.CompareTo(thirdNum) < 0
                            select new
                            {
                                First = firstNum,
                                Second = secondNum,
                                Third = thirdNum
                            }).ToArray();

            if (0 == triplets.Length)
            {
                throw new ArgumentException("Nested collection provided does not contain at least three distinct elements");
            }

            return (from n in nestedCollection
                    where n != null
                    from t in triplets
                    where n.Contains(t.First) && n.Contains(t.Second) && n.Contains(t.Third)
                    group n by t into g
                    orderby g.Count() descending
                    select new Triplets<T>
                    {
                        Item = g.Key.First,
                        Item2 = g.Key.Second,
                        Item3 = g.Key.Third,
                        Frequency = g.Count()
                    }).ToList();
        }
    }
}
