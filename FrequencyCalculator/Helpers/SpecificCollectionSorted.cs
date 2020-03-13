using System;
using System.Collections.Generic;

namespace FrequencyCalculator.Helpers
{
    internal static class SpecificCollectionSorted
    {
        /// <summary>
        /// Uses binary search algorithm to find a distinct group of specific items in a nested collection that contains
        /// sorted sub collections
        /// </summary>
        /// <typeparam name="T"> Type of base element </typeparam>
        /// <param name="nestedCollection"> Collection of collections to iterate through to find distinct group frequency </param>
        /// <param name="group">            group of elements to find the frequency of </param>
        /// <returns> New group object of the group to find and their frequency </returns>
        internal static int CalculateFrequencyOfGroup<T>(IEnumerable<IList<T>> nestedCollection,
                                                         IList<T> group) where T : IComparable<T>, IEquatable<T>
        {
            int count = 0;
            foreach (var collection in nestedCollection)
            {
                var length = collection.Count;
                var groupCounter = 0;

                for (var i = 0; i < group.Count; i++)
                {
                    bool found = false;
                    int index = BinarySearchHelper.BinarySearch(collection, 0, length - 1, group[i]);

                    if (index >= 0) { found = true; }

                    // If the value is not found in the collection
                    if (!found) { continue; }
                    else { groupCounter++; }
                }
                if (groupCounter == group.Count) { count++; }
            }
            return count;
        }
    }
}
