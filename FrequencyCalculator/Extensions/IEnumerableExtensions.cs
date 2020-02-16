using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Checkes to see if IEnumerable contains a nested IEnumerable. Ignores string types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns>bool true if nested</returns>
        public static bool IsNested<T>(this IEnumerable<T> input)
        {
            if(input.Any(x => x is null))
            {
                return false;
            }
            return (input.Any(x => x.GetType().GetInterfaces()
             .Any(i => i.IsGenericType
             && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
             && input.Any(x => x.GetType() != typeof(string)));
        }
        /// <summary>
        /// Flattens any nested enumerable passed. Ignores strings.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable Flatten(this IEnumerable enumerable)
        {
            foreach (object element in enumerable)
            {
                IEnumerable candidate = element as IEnumerable;
                if (candidate != null && candidate.GetType() != typeof(string))
                {
                    foreach (object nested in candidate)
                    {
                        yield return nested;
                    }
                }
                else
                {
                    yield return element;
                }
            }
        }
    }
}
