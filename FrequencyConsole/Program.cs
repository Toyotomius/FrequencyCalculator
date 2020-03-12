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

            var testSingleList = new List<TestSingle> { new TestSingle { First = 1, Second = 2 },
                                                        new TestSingle { First = 2, Second = 4 }, 
                                                        new TestSingle { First = 1, Second = 2 },
                                                        new TestSingle { First = 1, Second = 2 },
                                                        new TestSingle { First = 1, Second = 3 },
                                                        new TestSingle { First = 2, Second = 4 }, };

            //var results = testSingleList.CalculateSingles<TestSingle>();

            //foreach(var itm in results)
            //{
            //    Console.WriteLine($"{itm.Item.First} :: {itm.Frequency}"
            //        );
            //}
            //Expression<Func<object>> fn = () => (from item in testSingleList.GroupBy(x => x, new TestSingle.TestSingleComparer())


            //                                     orderby item.Count() descending
            //                                     select new Singles<TestSingle>
            //                                     {
            //                                         Item = item.Key,
            //                                         Frequency = item.Count()
            //                                     });


            //var group = (from item in testSingleList.GroupBy(x => x, new TestSingle.TestSingleComparer())



            //                                     orderby item.Count() descending
            //                                     select new Singles<TestSingle>
            //                                     {
            //                                         Item = item.Key,
            //                                         Frequency = item.Count()
            //                                     });


            testSingleList.Sort();
            var toFind = new TestSingle { First = 1, Second = 2 };
            var group = testSingleList.CalculateSingles(toFind, true);

            
            
                Console.WriteLine($"{ group.Item.First} :: {group.Item.Second} :: {group.Frequency}" );

            

            //TODO: Set up check for binary search and nested collections.
        }

        public class TestSingle : IComparable<TestSingle>, IEquatable<TestSingle>
        {
            public int First { get; set; }
            public int? Second { get; set; }

            public int CompareTo(TestSingle other)
            {
                return this.First.CompareTo(other.First);
            }
            

            public bool Equals([AllowNull] TestSingle other)
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
