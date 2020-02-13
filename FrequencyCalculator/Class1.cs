using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    public interface ICalculateFrequency<T>
    {
        public List<Singles<T>> CalculateSingles(IEnumerable<T> flatList);
    }

    public class Singles<T>
    {
        public int Frequency { get; set; }
        public T Item { get; set; }
    }

    public class CalculateSingleFrequency<T> : ICalculateFrequency<T>
    {
       public List<Singles<T>> CalculateSingles(IEnumerable<T> flatList)
        {
            var query = (from item in flatList
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
