# C# and .NET essentials for server programming

---

## Properties

A flexible and concise **way of manipulating** and **reading private fields**

Are actually special methods called accessors

Can be **read-write **/ **read-only** / **write-only**

```C#
public class Game {
    // An auto-implemented read-write property
    public int PlayerCount { get; set; }

    private bool _gameOver;
    // A read-write property with explicit backing field
    public bool GameOver {
        get { return _gameOver; }
        set { _gameover = value; }
    }
}
```

---

## Interfaces

Similar to a class – but **provides a specification** rather than an implementation

Members are **always public**

Members can be **methods**, **properties**, **events** or **indexers**

Members will be implemented by the classes and structs that implement the interface

```C#
public interface IEnumerator {
    bool MoveNext();
    object Current { get; }
}
```

---

### Implementing an interface

Done by providing an implementation for all members

```C#
public class Countdown : IEnumerator {
    int count = 11;
    public bool MoveNext () { return count-- > 0 ; }
    public object Current { get { return count; } }
}
```

A class (or struct) can implement multiple interfaces

---

### Interfaces: benefits

Write reusable code

- For objects created from classes **that implement the same interface**

---

```C#
public interface IAnimal {
    void MakeSound();
}
public class Cat : IAnimal {
    public void MakeSound(){
        // meow
    }
}
public class Dog : IAnimal {
    public void MakeSound() {
        // woof
    }
}
```

You can call this with object of **Cat** or **Dog**:

```cs
public MakeNoise(IAnimal animal) {
	animal.MakeSound();
}
```

---

### So why reusable code?

Change application behavior by changing the implementation <!-- .element: class="fragment" -->

Make testing easier by creating mock implementations <!-- .element: class="fragment" -->

---

## Exceptions

A **try** statement specifies a **code block subject to error-handling or cleanup code**

The **catch** block executes **when exception is thrown** in the try block

```C#
try {
	DoStuff();
	DoSomethingElse();
}
catch(Exception e) {
	Console.WriteLine("Error happened: " +e)
}
```

---

### Example of a catch clause

```C#
static void Main (string[] args) {
	try {
		byte b = byte.Parse (args[0]);
		Console.WriteLine (b);
	}
	catch (IndexOutOfRangeException ex) {
		Console.WriteLine ("Please provide at least one argument");
	}
	catch (FormatException ex) {
		Console.WriteLine ("That's not a number!");
	}
	catch (Exception ex) {
		Console.WriteLine ("We caught something else)
	}
}
```

---

### The catch clause

Specifies what type of exception to catch  <!-- .element: class="fragment" -->

Must either be System.Exception or a subclass of System.Exception  <!-- .element: class="fragment" -->

Catching System.Exception catches all possible errors  <!-- .element: class="fragment" -->

---

### Choosing the catch block

Evaluating which catch clause to execute is done from top to bottom <!-- .element: class="fragment" -->

Only one catch clause is executed  <!-- .element: class="fragment" -->

More specific exceptions must be placed above the more generic <!-- .element: class="fragment" -->

Typically you catch more specific exception types  <!-- .element: class="fragment" -->

- Avoids dealing with situations you were not expecting (e.g. OutOfMemoryException)  <!-- .element: class="fragment" -->

---

### The finally block

Finally block is always run, either:

- After a catch block finishes
- After control leaves the try block because of a jump statement (return etc.)
- After the try block ends

Typically used for cleanup:

```C#
static void ReadFile() {
	StreamReader reader = null;
	try {
		reader = File.OpenText ("file.txt");
		if (reader.EndOfStream) return;
		Console.WriteLine (reader.ReadToEnd());
	} finally {
		if (reader != null) reader.Dispose();
	}
}
```

---

### Throwing exceptions

```C#
static void Display (string name) {
	if (name == null) throw new ArgumentNullException ("name");
	Console.WriteLine (name);
}
```

Exceptions can be rethrown:

```C#
try {
	Display("John");
}
catch (ArgumentNullException e) {
	Console.WriteLine("Error: " e.Message);
	throw e;
}
```

---

### Key properties in an exception

**Stacktrace** <!-- .element: class="fragment" -->

- a string representing all the methods that are called from the origin of the exception to the catch block <!-- .element: class="fragment" -->

**Message** <!-- .element: class="fragment" -->

- A string with a description of the error <!-- .element: class="fragment" -->

**InnerException** <!-- .element: class="fragment" -->

- The inner exception (if any) that caused the outer exception <!-- .element: class="fragment" -->

---

## Tasks and asynchronous programming

Servers often need to deal with more than one thing happening at a time (concurrency) <!-- .element: class="fragment" -->

Multithreading is the way of creating concurrency <!-- .element: class="fragment" -->

- Tasks are a higher-level abstraction on top of threads <!-- .element: class="fragment" -->

Many libraries provide asynchronous versions of operations which utilize Tasks <!-- .element: class="fragment" -->

Thus, it’s important to know the basics of using Tasks <!-- .element: class="fragment" -->

---

### Asynchronous functions

**async** and **await** are keywords used for asynchronous programming in C#

- These let you write asynchronous code with **same structure and simplicity as synchronous code**
  
You can apply await-keyword to tasks:

```C#
public async Task GetPlayerAndPrintNameAsync(Guid id) {
    // GetPlayerAsync returns a Task<IPlayer>
	IPlayer player = await database.GetPlayerAsync(id); 
	Console.WriteLine("Got player named: " +player.Name);
}

// Synchronous version of the same method:
public void GetPlayerAndPrintName(Guid id) {
	IPlayer player = database.getPlayer(id);
	Console.WriteLine("Got player named: " +player.Name);
}
```

---

### Asynchronous functions (2)

The **async** modifier can be applied only to methods that return ```void or Task or Task<TResult>```

- There is virtually no reason to return void ever though

You can create your own asynchronous functions but that is out of the scope of this course

---

## Json (JavaScript object notation)

A simple example:

```json
{
	"_id" : "57ade8face4bf5361c4268d6", 
	"Name" : "john",
	"Score" : 7,  
	"Items" : [
		{ 
			"_id" : "57ade921ce4bf5361c4268d7", 
			"ItemType" : 0 
		} 
	] 
}
```

---

### Json

Notation used to **describe objects**

Many APIs communicate in JSON or XML (or both)

**Language independent**

Has the following datatypes:

- Number
- Object
- Array
- Boolean
- null

Newtonsoft.Json library is often used for handling JSON data with .NET
