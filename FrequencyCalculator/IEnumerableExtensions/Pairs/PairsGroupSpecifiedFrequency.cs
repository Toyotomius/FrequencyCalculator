using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsGroupSpecifiedFrequency
    {
        public static IEnumerable<Pairs<T>> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection,
                                                                    IEnumerable<IList<T>> pairGroup) where T : IComparable<T>, IEquatable<T>
        {
            var index = 0;
            foreach (var pair in pairGroup)
            {
                if (pair is null) { index++; continue; }

                var distinct = pair.Distinct().Where(x => x is object);
                if (distinct.Count() != 2)
                {
                    throw new ArgumentException($"Group {index} of {nameof(pair)} in {nameof(pairGroup)} does not contain exactly three distinct values");
                }

                yield return (from n in nestedCollection
                              where n != null
                              where n.Contains(pair[0]) && n.Contains(pair[1])
                              group n by pair into g
                              orderby g.Count() descending
                              select new Pairs<T>
                              {
                                  Item = pair[0],
                                  Item2 = pair[1],
                                  Frequency = g.Count()
                              }).First();
                index++;
            }
        }
    }
}
