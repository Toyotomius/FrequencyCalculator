using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.Helpers
{
    internal static class SpecificSingleSorted
    {
        /// <summary>
        /// Uses binary search algorithm to find the frequency of a single value when passed a flat sorted collection.
        /// </summary>
        /// <typeparam name="T"> Primitive type of the elements in collection </typeparam>
        /// <param name="collection"> Flat sorted collection passed </param>
        /// <param name="value">      Value to find the frequency of </param>
        /// <returns>
        /// New Singles object with the value and number of occurrences. Will return Item as null if passed null
        /// </returns>
        internal static Singles<T> CalculateSinglesSorted<T>(IList<T> collection, T value) where T : IComparable<T>, IEquatable<T>
        {
            var length = collection.Count;
            int index = BinarySearchHelper.BinarySearch(collection, 0, length - 1, value);

            // If the value is not found in the collection
            if (index == -1)
            {
                return new Singles<T> { Item = value, Frequency = 0 };
            }

            // When an element is found count all occurrences to the left
            var count = 1;
            var left = index - 1;
            while (left >= 0 && collection[left].Equals(value))
            {
                count++;
                left--;
            }

            // Then count all occurrences to the right
            var right = index + 1;
            while (right < length && collection[right].Equals(value))
            {
                count++;
                right++;
            }

            return new Singles<T> { Item = value, Frequency = count };
        }
    }
}
