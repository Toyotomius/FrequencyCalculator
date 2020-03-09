using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator.IEnumerableExtensions
{
    public static class TripletSpecifiedFrequency
    {
        public static Triplets<T> CalculateTriplets<T>(this IEnumerable<IEnumerable<T>> nestedCollection, IList<T> triplet) where T : IComparable
        {
            var distinctTriplet = triplet.Distinct();
            if(distinctTriplet.Count() != 3) { throw new ArgumentException($"{nameof(triplet)} does not contain exactly three distinct values"); }
            foreach(var num in triplet)
            {
                if(num is null) { return new Triplets<T> { }; }
            }

            return (from n in nestedCollection
                    where n != null
                    from t in triplet
                    where n.Contains(triplet[0]) && n.Contains(triplet[1]) && n.Contains(triplet[2])
                    group n by t into g
                    orderby g.Count() descending
                    select new Triplets<T>
                    {
                        Item = triplet[0],
                        Item2 = triplet[1],
                        Item3 = triplet[2],
                        Frequency = g.Count()
                    }).First();
        }
    }
}
