# FrequencyCalculator

The frequency calculator is a collection of generic IEnumerable extension methods used to calculate the frequencies
of **distinct** elements in collections. Best used with simple types but can also be used with custom types that implement
IComparable<T>, IEquatable<T> and override GetHashCode when implementing IEquatable.

#### Methods:
* Flatten
* CalcualteSingles
* CalculatePairs
* CalculateTriplets

#### To use methods add using statement:
```
using FrequencyCalculator.IEnumerableExtensions;
```

### IEnumerable\<T> Flatten<T>(this IEnumerable enumerable)

Takes a nested collection and returns a single IEnumerable<T> collection. Does not flatten strings into individual
characters.

Example:
```
var list = new List<List<int>>
{ 
  new List<int> {1, 2, 3, 4},
  new List<int> {3, 6, 2, 8}
};

var result = list.Flatten<int>();

result == IEnumerable<int> {1, 2, 3, 4, 3, 6, 2, 8};
```

### List<Singles\<T>> CalculateSingles\<T>(this IEnumerable collection) where T : IComparable<T>, IEquatable<T>

Extension method that takes a collection or nested collection (which it then flattens) and calculates the frequency
of each individual element in the collection. Ignores null values.

Returns a list of Singles objects in descending order:
```
var singlesList = new List<string> {"1", null};
var results = singlesList.CalculateSingles<string>();

results == new List<Singles>
{
  new Singles<int>
  {
    Item = "1",
    Frequency = 1
  }
};
// null value is discarded.
```
```
var singelsList = new List<int> {1, 2, 2}; 
var results = singlesList.CalculateSingles<int>();

results == new List<Singles>
{
  new Singles<int>
  {
    Item = 2,
    Frequency = 2
  },
  new Singles<int>
  {
    Item = 1,
    Frequency = 1
  }
}
```


Contains overloads for:

##### Finding a specific frequency
```
var results = singlesList.CalculateSingles(1);

results == new Singles<int>
{
  Item = 1,
  Frequency = 1
}
```
Returns a Singles object of only the element searched for and its frequency. If the element is not in the collection,
returns a frequency of 0.

##### Finding a group of specific frequencies
```
var itemsToFind = new List<int> {1, 4};
var singlesList = new List<int> {1, 1, 4, 5, 5};
var results = singlesList.CalculateSingles(itemsToFind);

results == new IEnumerable<Singles<int>>
{
  new Singles<int>
  {
    Item = 1,
    Frequency = 2
  }
  new Singles<int>
  {
    Item = 4,
    Frequency = 1
  }
};
```
#### Note: When finding the frequencies of a group of items, method returns an IEnumerable and uses deferred execution.

Both of these overload methods also have a boolean overload, IsSorted, which when passed as true uses binary search instead.
```
singlesList.CalculateSingles(3, true); // False by default.
```

This overload will not work with nested collections as there is no guarantee of order during the flattening process.

### List<Pairs\<T>> CalculatePairs\<T>(this IEnumerable<IEnumerable\<T>> nestedCollection) where T : IComparable<T>, IEquatable<T>

Takes a 2D collection and calculates frequencies of each **distinct** pair of elements to find how frequently these
pairs show up in each separate sub-collection. If the collection does not contain at least two distinct elements, it will
throw an Argument Exception.

Example:
```
var pairsList = new List<List<int>>
{
  new List<int> {1, 2},
  new List<int> {1, 2}
};
var results = pairsList.CalculatePairs<int>();
results == new List<Pairs<int>>
{
  Item = 1,
  Item2 = 2,
  Frequency = 2
}
```
Does not flatten and as such collections of nesting greater than 2D are not compatible.

The overloads are the same as they are for CalculateSingles:
```
var pair = new List<int> {1,2};
var results = pairsList.CalculatePairs(pair);
```
```
var results = pairsList.CalculatePairs(pair, true);
```
```
var itemsToFind = new List<List<int>>
{
  new List<int> {1, 2},
  new List<int> {3, 4}
}
var results = pairsList.CalculatePairs(itemsToFind);
```
#### Note: When finding the frequencies of a group of items, method returns an IEnumerable and uses deferred execution.

The overloads will throw Argument Exception if the items passed do not contain two distinct elements to find.

Ignores null items in the collection passed in the same way as CalculateSingles.

Will also ignore nulls passed as part of the specific pair to search for:
```
var itemsToFind = new List<string> {"1", "2", null};
var results = itemsToFind.CalculatePairs(itemsToFind); // Will discard the null value
```
However if the discarded null results in less than two distinct elements the method will throw a new ArgumentException:
```
var itemsToFind = new List<string> {"1", null};
var results = itemsToFind.CalculatePairs(itemsToFind); // <--- Will throw a new Argument Exception
```

### List<Triplets\<T>> CalculateTriplets\<T>(this IEnumerable<IEnumerable\<T>> nestedCollection) where T : IComparable<T>, IEquatable<T>

Takes a 2D collection and calculates frequencies of each **distinct** triplets of elements to find how frequently these
triplets show up in each separate sub-collection. If the collection does not contain at least three distinct elements, it will
throw an Argument Exception.

Example:
```
var tripletList = new List<List<int>>
{
  new List<int> {1, 2, 3},
  new List<int> {1, 2, 3}
};
var results = tripletList.CalculateTriplets<int>();
results == new List<Triplets<int>>
{
  Item = 1,
  Item2 = 2,
  Item3 = 3,
  Frequency = 2
}
```
Does not flatten and as such collections of nesting greater than 2D are not compatible.

The overloads are the same as they are for CalculateSingles:
```
var triplet = new List<int> {1,2,3};
var results = tripletList.CalculateTriplets(triplet);
```
```
var results = tripletList.CalculateTriplets(triplet, true);
```
```
var itemsToFind = new List<List<int>>
{
  new List<int> {1, 2, 3},
  new List<int> {3, 4, 5}
}
var results = tripletList.CalculateTriplets(itemsToFind);
```
#### Note: When finding the frequencies of a group of items, method returns an IEnumerable and uses deferred execution.

The overloads will throw Argument Exception if the items passed do not contain three distinct elements to find.

Ignores null items in the collection passed in the same way as CalculateSingles.

Will also ignore nulls passed as part of the specific triplet to search for:
```
var itemsToFind = new List<string> {"1", "2", "3", null};
var results = itemsToFind.CalculateTriplets(itemsToFind); // Will discard the null value
```
However if the discarded null results in less than three distinct elements the method will throw a new ArgumentException:
```
var itemsToFind = new List<string> {"1", null, "3"};
var results = itemsToFind.CalculateTriplets(itemsToFind); // <--- Will throw a new Argument Exception
```

All **Calculate** methods will work with custom types that implement IComparable\<T> and IEquatable\<T>:

```
public class CustomClass : IComparable<CustomClass>, IEquatable<CustomClass>
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int CompareTo(CustomClass other)
        {
            return this.First.CompareTo(other.First);
        }

        public bool Equals(CustomClass other)
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
```
```
var valuesPassed = new List<CustomClass>
  {
    new TestClass { First = 1, Second = 2 },
    new TestClass { First = 1, Second = 2 },
    new TestClass { First = 1, Second = 1 },    
  };
var result = valuesPassed.CalculateSingles<CustomClass>();

var result == new List<Singles<CustomClass>>
  {
    new Singles<TestClass> { Item = new CustomClass { First = 1, Second = 2 }, Frequency = 2 },
    new Singles<TestClass> { Item = new CustomClass { First = 1, Second = 1 }, Frequency = 1 }
  };
 ``` 
 
