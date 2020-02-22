using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.Helpers
{
    internal static class SinglesSorted
    {
        /// <summary>
        /// Uses binary search algorithm to find the frequency of a single value when passed a flat
        /// sorted collection.
        /// </summary>
        /// <typeparam name="T"> Primitive type of the elements in collection </typeparam>
        /// <param name="collection"> Flat sorted collection passed </param>
        /// <param name="value">      Value to find the frequency of </param>
        /// <returns> New Singles object with the value and number of occurrences </returns>
        internal static Singles<T> CalculateSinglesSorted<T>(IList<T> collection, T value) where T : IComparable
        {
            var length = collection.Count;
            int index = BinarySearch(collection, 0, length - 1, value);

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

        /// <summary>
        /// Binary search method helper for calculating individual frequency of a specified value
        /// </summary>
        /// <typeparam name="T"> Primitive type of the elements in collection </typeparam>
        /// <param name="collection"> Flat sorted collection passed </param>
        /// <param name="left">       Index value of the lowest point for the binary search </param>
        /// <param name="right">      Index value of the highest point for the binary search </param>
        /// <param name="value">      Value to find the frequency of </param>
        /// <returns>
        /// Returns the index once the value is found in the collection. Otherwise recurses to find
        /// the value
        /// </returns>
        private static int BinarySearch<T>(IList<T> collection, int left, int right, T value) where T : IComparable
        {
            // Return -1 when the right most bound passes the left when the value is not found
            if (right < left)
            {
                return -1;
            }

            var mid = left + ((right - left) / 2);

            // Return the element once it is equal to the middle element
            if (collection[mid].Equals(value))
            {
                return mid;
            }

            // If element is smaller than mid, search left of mid
            if (collection[mid].CompareTo(value) > 0)
            {
                return BinarySearch(collection, left, mid - 1, value);
            }

            // Else search right of mid
            return BinarySearch(collection, mid + 1, right, value);
        }
    }
}
