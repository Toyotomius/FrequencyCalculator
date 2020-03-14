using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyCalculator.Helpers
{
    internal static class BinarySearchHelper
    {
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
        internal static int BinarySearch<T>(IList<T> collection, int left, int right, T value) where T : IComparable<T>, IEquatable<T>
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
