using System;

namespace FrequencyCalculator.DataModels
{
    /// <summary>
    /// Object that contains individual item and frequency of item. Takes item type from caller.
    /// </summary>
    /// <typeparam name="T">Type provided by caller</typeparam>
    public class Singles<T>
    {
        private T _Item;
        private string _ItemForNullStrings ="";
        public int Frequency { get; set; }

        public T Item
        {
            get
            {
                if (_ItemForNullStrings.Length > 0)
                {
                   var _item = (T)Convert.ChangeType(_ItemForNullStrings, typeof(T));
                    return _item;
                }
                else
                {
                    return _Item;
                }
            }
            set
            {
                if (value is null)
                {
                    _ItemForNullStrings = "Null String";
                }
                else
                {
                    _Item = value;
                }
            }
        }
    }
}
