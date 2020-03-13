using System;

namespace FrequencyCalculator.Tests
{
    public class TestClass : IComparable<TestClass>, IEquatable<TestClass>
    {
        public int First { get; set; }

        public int? Second { get; set; }

        public int CompareTo(TestClass other)
        {
            return this.First.CompareTo(other.First);
        }

        public bool Equals(TestClass other)
        {
            return this.First.Equals(other.First) && this.Second.Equals(other.Second);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + First.GetHashCode();
            hash = hash * 23 + Second.GetHashCode();
            return hash;
        }
    }
}
