using FrequencyCalculator;
using FrequencyCalculator.DataModels;
using FrequencyCalculator.IEnumerableExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace FrequencyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var watch = new Stopwatch();
            var rand = new Random();

            //string json = "";
            //using (var sr = new StreamReader("sortedlist.json"))
            //{
            //    json = sr.ReadToEnd();
            //}

            //var list = JsonConvert.DeserializeObject<List<int>>(json);
            //watch.Start();
            //var result = list.CalculateSingles<int>();
            //watch.Stop();
            //foreach(var itm in result)
            //{
            //    Console.WriteLine($"{itm.Item}  : {itm.Frequency}");
            //}

            //Console.WriteLine(watch.ElapsedMilliseconds);

            var valuesPassed = new List<List<TestClass>>
            {
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4 }, new TestClass { First = 5, Second = 6 }  },
                new List<TestClass>{ new TestClass { First = 3, Second = 4 }, new TestClass { First = 2, Second = 3 }, new TestClass { First = 2, Second = 4 }  },

            };
            var valueToFind = new List<List<TestClass>> {
                new List<TestClass> { new TestClass { First = 1, Second = 2 }, new TestClass { First = 3, Second = 4  } }
            };
            var group = valuesPassed.CalculateTriplets(valueToFind);

            foreach (var itm in group)
            {
                Console.WriteLine($"{itm.Item.First} :: {itm.Item.Second} /// {itm.Item2.First} -- {itm.Item2.Second} :::: {itm.Frequency}");
            }


            //TODO: Set up check for binary search and nested collections.
            //TODO: Set up binary search for pairs & triplets.
        }

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
}
