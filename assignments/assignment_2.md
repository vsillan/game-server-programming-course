# Assignment 2

The purpose of the following exercise is to get you familiar with .NET concepts to get you ready to do some serious server programming.

## Preparation

### Create a new project

Create a new console application that can be used to run the following exercises. You might remember from the previous assignment that you can create a new console project with `dotnet new console` command.

### Create two new classes and an interface

Create the following classes and interface in separate files inside the project folder.

```C#
public interface IPlayer
{
    int Score { get; set; }
}

public class Player : IPlayer
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public List<Item> Items { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    public int Level { get; set; }
}
```

## 1. Guid

Implement a function that instantiates 1 000 000 `Player` objects with random Guid as the Id.

Write code that checks that there are no duplicate Ids among the `Player` objects.

## 2. Extension method

Implement an extension method `GetHighestLevelItem` for `Player` which returns the `Item` with highest level from the item list.

Write code that creates a player with various items and use the `GetHighestLevelItem` function to find the highest level item and print the result.

## 3. LINQ

Create two variations of a function that takes a `Player` as a parameter and returns all `Item` objects in the list as an array.

- `GetItems`: Transform the list to an array using normal C# loop to do the work and return it
- `GetItemsWithLinq`: Transform the list to an array by using LINQ extension methods and return it. You can find the documentation for Linq extension methods that are usable with enumerables (like lists) here: https://docs.microsoft.com/en-us/dotnet/api/system.linq?view=netcore-3.1

Write some runnable code that proves that these functions work.

## 4. LINQ 2

Create two variations of a function that takes a `Player` as a parameter and returns the first `Item` object in the `Item`-list owned by the player.

- `FirstItem`: Get the first item from a list and return it. If the list is empty, return null.
- `FirstItemWithLinq`: Find the LINQ extension method that does the same thing. You can find the documentation for Linq extension methods that are usable with enumerables (like lists) here: https://docs.microsoft.com/en-us/dotnet/api/system.linq?view=netcore-3.1

Write some runnable code that uses the functions.

## 5. Delegates

Implement a function with signature `void ProcessEachItem(Player player, Action<Item> process);`. This function should call the delegate on each of the `Item` objects owned by the `Player` object.

Write code that uses the `ProcessEachItem` function to print the `Id` and `Level` of each item to the console. You should write a function with signature `void PrintItem(Item item);` and pass the function as a parameter to `ProcessEachItem`.

## 6. Lambda

Call the `ProcessEachItem` function (implemented in the previous exercise) with a lambda function that does the same thing as the function in the previous excercise (so print the `Id` and `Level` to the console).

## 7. Generics

Create a generic class called `Game`:

```C#
public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players) {
        _players = players;
    }

    public T[] GetTop10Players() {
        // ... write code that returns 10 players with highest scores
    }
}
```

Implement the `GetTop10Players` method.

Write an another class that implements the IPlayer interface called `PlayerForAnotherGame`.

Write code that demonstrates that you can instantiate the generic `Game` class and call `GetTop10Players` with both `Player` and `PlayerForAnotherGame`.
