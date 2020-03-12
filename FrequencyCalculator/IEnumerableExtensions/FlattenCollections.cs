using System.Collections;
using System.Collections.Generic;

namespace FrequencyCalculator
{
    public static class FlattenCollections
    {
        /// <summary> Flattens collections passed using recursion. Ignores strings. </summary>
        /// <typeparam name="T"> Base type of the lowest elements of the nested collections </typeparam>
        /// <param name="enumerable"> The enumerable collection to be flattened </param>
        /// <returns> IEnumerable <T> where T: is the base type of the nested collections being passed </T> </returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable enumerable)
        {
            foreach (object? element in enumerable)
            {
                if (element is IEnumerable candidate && candidate.GetType() != typeof(string))
                {
                    foreach (T nested in Flatten<T>(candidate))
                    {
                        yield return nested;
                    }
                }
                else if (element is null)
                {
                    continue;
                }
                else
                {
                    yield return (T)element;
                }
            }
        }
    }
}
