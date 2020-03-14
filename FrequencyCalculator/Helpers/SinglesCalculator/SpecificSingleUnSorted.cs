using FrequencyCalculator.DataModels;
using System;
using System.Collections.Generic;

namespace FrequencyCalculator.Helpers
{
    public static class SpecificSingleUnSorted
    {
        /// <summary> Finds the frequency of a single distinct value when collection is unsorted. </summary>
        /// <typeparam name="T"> Primitive type of the elements in collection </typeparam>
        /// <param name="collection"> Flat sorted collection passed </param>
        /// <param name="value">      Value to find the frequency of </param>
        /// <returns>
        /// New Singles object with the value and number of occurrences. Will return Item as null if passed null
        /// </returns>
        internal static Singles<T> CalculateSinglesUnSorted<T>(IList<T> collection, T value) where T : IComparable<T>, IEquatable<T>
        {
            var count = 0;

            foreach (var itm in collection)
            {
                if (itm.Equals(value))
                {
                    count++;
                }
            }

            return new Singles<T> { Item = value, Frequency = count };
        }
    }
}
