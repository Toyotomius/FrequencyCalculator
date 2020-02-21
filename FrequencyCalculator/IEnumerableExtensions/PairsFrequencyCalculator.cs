using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class PairsFrequencyCalculator
    {
        public static List<Pairs<T>> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection) where T : IComparable
        {
            var distinct = nestedCollection.Where(x => x is object).SelectMany(x => x).Where(x => x is object).Distinct();

            var pairs = from firstNum in distinct
                        from secondNum in distinct
                        where firstNum.CompareTo(secondNum) < 0 && firstNum is object
                        select new
                        {
                            First = firstNum,
                            Second = secondNum
                        };

            return (from n in nestedCollection where n != null
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
