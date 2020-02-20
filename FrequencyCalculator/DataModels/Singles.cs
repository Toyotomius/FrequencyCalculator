namespace FrequencyCalculator.DataModels
{
    /// <summary>
    /// Object that contains individual item and frequency of item. Takes item type from caller.
    /// </summary>
    /// <typeparam name="T">Type provided by caller</typeparam>
    public class Singles<T>
    {
        public int Frequency { get; set; }

        public T Item { get; set; }
    }
}
