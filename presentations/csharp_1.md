# C# and .NET essentials for server programming

---

## Interfaces

```C#
public interface IEnumerator {
	bool MoveNext();
	object Current { get; }
}
```

Similar to a class – but it provides a specification rather than an implementation

Members are always public

Members will be implemented by the classes and structs that implement the interface

A class (or struct) can implement multiple interfaces

---

### Interfaces

```C#
public class Countdown : IEnumerator {
	int count = 11;
	public bool MoveNext () { return count-- > 0 ; }
	public object Current { get { return count; } }
}
```

Implementing an interface means providing a public implementation for all its members

You can implicitly cast an object to any interface that it implements:

```C#
IEnumerator e = new Countdown();
while (e.MoveNext())
Console.Write (e.Current); // 109876543210
```

---

### Interfaces: What can we do with them?

Utilizing the same code with all the classes that derive from same interface:

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
->

```C#
public MakeNoise(IList<IAnimal> animals) {
    foreach (IAnimal animal in animals)
        animal.MakeSound();
}
```

---

### Interfaces: What can we do with them?(2)

Replacing implementations without changing the user code:

```C#
IAnimal animal = new Dog();
animal.MakeSound(); // woof

// OR

IAnimal animal = new Cat();
animal.MakeSound(); // meow
```

(Imagine there is 1000 more lines of code using the IAnimal object

---

## Exceptions

A try statement specifies a code block subject to error-handling or cleanup code

The catch block executes when an error occurs in the try block

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

### The catch clause

A catch clause specifies what type of exception to catch

Must either be System.Exception or a subclass of System.Exception

Catching System.Exception catches all possible errors

Typically you catch more specific exception types

Avoids dealing with situations you were not expecting (e.g. OutOfMemoryException)

Only one catch clause is executed

---

### The catch clause(2)

Evaluating which catch clause to execute is done from top to bottom

More specific exceptions must be placed above the more generic

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
	catch (OverflowException ex) {
		Console.WriteLine ("You've given me more than a byte!");
	}
}
```

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
	Console.WriteLine("Error in displaying the name: " e.Message);
	throw;
}
```

---

### Key properties in an exception

Stacktrace - A string representing all the methods that are called from the origin of the exception to the catch block

Message - A string with a description of the error

InnerException - The inner exception (if any) that caused the outer exception

---

## Tasks and asynchronous programming 

All servers need to deal with more than one thing happening at a time (concurrency)

- Clients are sending requests simultaneously

Multithreading is the way of creating concurrency -> Tasks are a higher-level abstraction on top of threads

Many libraries provide asynchronous versions of operations which utilize Tasks

Thus, it’s important to know the basics of using Tasks

---

### Asynchronous functions

There are keywords async and await for asynchronous programming in C#

- Let you write asynchronous code with same structure and simplicity as synchronous code
  
You can apply await-keyword to tasks:

```C#
public async Task GetPlayerAndPrintNameAsync(Guid id) {
	IPlayer player = await database.GetPlayerAsync(id); // GetPlayerAsync returns a Task<IPlayer>
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

The async modifier can be applied only to methods (and lambda expressions) that return void or a Task or Task<TResult>

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

Notation used to describe objects

Language independent

Has the following datatypes:

- Number
- Object
- Array
- Boolean
- null

Many APIs communicate in JSON or XML (or both)

Many people use Newtonsoft.Json library for handling JSON data with .NET

---