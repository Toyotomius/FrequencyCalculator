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
            var distinct = nestedCollection.Where(x => x is object).SelectMany(x => x).Where(x => x is object).Distinct().ToArray();

            var pairs = (from firstNum in distinct
                        from secondNum in distinct
                        where firstNum.CompareTo(secondNum) < 0 && firstNum is object
                        select new
                        {
                            First = firstNum,
                            Second = secondNum
                        }).ToArray();

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

        public static Pairs<T> CalculatePairs<T>(this IEnumerable<IEnumerable<T>> nestedCollection, IList<T> pair) where T : IComparable
        {




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

//TODO: See if intersects would be more performant.
