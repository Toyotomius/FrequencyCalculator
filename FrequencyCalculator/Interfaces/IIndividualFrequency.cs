using FrequencyCalculator.DataModels;
using System.Collections;
using System.Collections.Generic;


namespace FrequencyCalculator
{
    public interface IIndividualFrequency<T>
    {
        //public List<Singles<T>> CalculateNestedSingles(IEnumerable<IEnumerable<T>> nestedList);

        public List<Singles<T>> CalculateSingles(IEnumerable collection);
    }
}
