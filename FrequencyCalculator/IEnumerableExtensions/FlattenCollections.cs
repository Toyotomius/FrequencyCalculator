using System.Collections;
using System.Collections.Generic;

namespace FrequencyCalculator
{
    public static class FlattenCollections
    {
        /// <summary>
        /// Flattens collections passed using recursion. Ignores strings.
        /// </summary>
        /// <typeparam name="T"> Base type of the lowest elements of the nested collections </typeparam>
        /// <param name="enumerable"> The enumerable collection to be flattened </param>
        /// <returns> IEnumerable <T> where T: is the base type of the nested collections being passed </T> </returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable enumerable)
        {
            foreach (object element in enumerable)
            {
                IEnumerable candidate = element as IEnumerable;
                if (candidate != null && candidate.GetType() != typeof(string))
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

        /// <summary>
        /// Checkes to see if IEnumerable contains a nested IEnumerable. Ignores string types.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="input"> </param>
        /// <returns> bool true if nested </returns>
        //public static bool IsNested<T>(this IEnumerable<T> input)
        //{
        //    if (input.Any(x => x is null))
        //    {
        //        return false;
        //    }
        //    return (input.Any(x => x.GetType().GetInterfaces()
        //     .Any(i => i.IsGenericType
        //     && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
        //     && input.Any(x => x.GetType() != typeof(string)));
        //}
    }
}
