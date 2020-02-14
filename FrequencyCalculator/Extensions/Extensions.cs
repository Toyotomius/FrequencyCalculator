using System.Collections.Generic;
using System.Linq;

namespace FrequencyCalculator
{
    public static class Extensions
    {
        /// <summary>
        /// Checkes to see if IEnumerable contains a nested IEnumerable. Ignores string types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns>bool true if nested</returns>
        public static bool IsNested<T>(this IEnumerable<T> input)
        {
            return (input.Any(x => x.GetType().GetInterfaces()
             .Any(i => i.IsGenericType
             && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
             && input.Any(x => x.GetType() != typeof(string)));
        }
    }
}
