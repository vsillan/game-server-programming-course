# C# and .NET essentials for server programming

## part 2

---

## Guid

Globally unique identifier <!-- .element: class="fragment" -->

Example: “644e1dd7-2a7f-18fb-b8ed-ed78c3f92c2b” <!-- .element: class="fragment" -->

128-bit integer (16 bytes) <!-- .element: class="fragment" -->

Very low probability of being duplicate <!-- .element: class="fragment" -->

Creation: Guid id = Guid.NewGuid(); <!-- .element: class="fragment" -->

Note:

Probablity: This number is equivalent to generating 1 billion UUIDs per second for about 85 years, and a file containing this many UUIDs, at 16 bytes per UUID, would be about 45 exabytes, many times larger than the largest databases currently in existence, which are on the order of hundreds of petabytes. (Wikipedia)

---

## Extension methods

Allow extending a type with new methods without altering the original definition <!-- .element: class="fragment" -->

An extension method is a static method of a static class, where the this modifier is applied to the first parameter <!-- .element: class="fragment" -->

---

### Extension method example

```C#
public static class StringHelper {
	public static bool IsCapitalized (this string s) {
		if (string.IsNullOrEmpty(s)) return false;
		return char.IsUpper (s[0]);
	}
}
```

Can be called like this:

```C#
bool isCapitalized = “John”.IsCapitalized();
```

Note:

Needed for doing the .NET Core middleware registration in the designed way

Just syntactic sugar

---

## Attributes

```C#
[Route("/api/players/", Name = "CreatePlayerRoute")]
[ValidateModel]
[HttpPost]
public async Task<IHttpActionResult> CreatePlayer(
    HttpRequestMessage requestMessage,
    NewPlayer newPlayer) { ...}
```

Attributes are an extensible mechanism for adding custom information to code elements <!-- .element: class="fragment" -->

Attribute parameters fall into one of two categories: positional or named <!-- .element: class="fragment" -->

- In the “Route” attribute above, the first argument is a positional parameter; the second is a named parameter <!-- .element: class="fragment" -->

Note:

You can create your own attributes by extending System.Attribute

- All attribute type names should end with a word “Attribute” by convention

You can specify multiple attributes for a single code element

Positional parameters correspond to parameters of the attribute type’s public constructors. Named parameters correspond to public fields or public properties on the attribute type.

---

## LINQ

Language-integrated query <!-- .element: class="fragment" -->

Extension methods (and query expressions) for querying collections and other data sources <!-- .element: class="fragment" -->

Basically replacing foreach loops with the LINQ approach <!-- .element: class="fragment" -->

Very common to see LINQ being used in C# code <!-- .element: class="fragment" -->

---

### LINQ example

Typical examples are .ToArray() and .First()

Easily converting a list to an array:

```C#
var list = new List<int>();
var array = list.ToArray();
```

---

## Delegates

A delegate is an **object that knows how to call a method**

The following defines a delegate type called Transformer:

```C#
delegate int Transformer (int x);
```

A delegate type defines the kind of method that delegate instances can call:

- Return type
- Parameters

---

### Delegates

Transformer is compatible with any method with an int return type and a single int parameter:

```C#
static int Square (int x) { return x * x; }
```

Assigning a method to a delegate variable creates a delegate instance

```C#
Transformer t = Square;
```

which can be invoked in the same way as a method

```C#
int answer = t(3); // answer is 9
```

---

### Delegates

A delegate instance literally acts as a delegate for the caller

- The caller invokes the delegate
- The delegate calls the target method

Decouples the caller from the target method

```C#
app.Use(async (context, next) => {
	// Do work before next middleware
	await next.Invoke(); // We don't need to know what "next" is here.
	// Do work after the next middleware returns
});
```

---

## Action-delegate

Action is a delegate type defined in the System namespace

```C#
public delegate void Print(string value);

public static void ConsolePrint(string value) {
    Console.WriteLine(value);
}

public static void PrintTest() {
    Print printFunction = ConsolePrint;
    printFunction("test");
}

public static void PrintTestWithAction() {
    Action<string> printFunction = ConsolePrint;
    printFunction("test");
}
```

Note:

- Can have up to 16 parameters

---

## Func-delegate

Func is the similar as Action-delegate but it **has a return value**

`Func<int>` is a delegate that takes a function that:

- has no parameters
- returns an `int`

`Func<int, int>` is a delegate that takes a function that:

- has one parameter with `int`-type
- returns an `int`

---

## Lambda expressions

A lambda expression is an **unnamed method written in place of a delegate instance**

A lambda expression has the following form:

- (parameters) => expression-or-statement-block

An example of a lambda expression:

```C#
x => x * x
// ‘function’ that has a parameter x in which x
// is multiplied with x and the result is returned
```

A lambda expression can be a statement block instead of an expression

```C#
x => { return x * x; };
```

Note:

Parentheses can be omitted if there is exactly one parameter

---

## Generics

```C#
public class Stack<T> {
	int position;
	T[] data = new T[100];
	public void Push (T obj) { data[position++] = obj; }
	public T Pop() { return data[--position]; }
}

```

`Stack<int>` fills in the type parameter T with the type argument int

`Stack<T>` is an open type, whereas `Stack<int>` is a closed type

At runtime, all generic type instances are closed

Note:

Explain with an example why generics are useful

Why not hardcode a separate version for each type

Why not have just a stack with type “object”

---

### Generics

A **mechanism for writing reusable code** across different types

Generics, when compared to inheritance, can increase type safety and reduce casting and boxing

Classes, interfaces and methods can be generic
