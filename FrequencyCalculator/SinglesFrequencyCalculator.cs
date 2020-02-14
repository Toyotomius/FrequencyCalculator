using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    public interface IIndividualFrequency<T>
    {
        public List<Singles<T>> CalculateNestedSingles(IEnumerable<IEnumerable<T>> nestedList);

        public List<Singles<T>> CalculateSingles(IEnumerable<T> flatList);
    }

    public class CalculateIndividualFrequency<T> : IIndividualFrequency<T>
    {
        public List<Singles<T>> CalculateNestedSingles(IEnumerable<IEnumerable<T>> nestedList)
        {
            var query = (from number in nestedList.SelectMany(n => n)
                         group number by number into g
                         orderby g.Count() descending
                         select new Singles<T>
                         {
                             Item = g.Key,
                             Frequency = g.Count()
                         }).ToList();
            return query;
        }

        public List<Singles<T>> CalculateSingles(IEnumerable<T> list)
        {
            if (list.IsNested())
            {
                throw new System.ArgumentException("Use CalculatedNestedSingles for nested lists where individual frequency is desired",
                    nameof(list));
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

    public class Singles<T>
    {
        public int Frequency { get; set; }
        public T Item { get; set; }
    }
}
