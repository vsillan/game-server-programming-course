# Assignment 2

The purpose of the following exercise is to get you familiar with .NET concepts to get you ready to do some serious server programming.

## Preparation

### Create a new project

Create a new console application that can be used to run the following exercises. You can find guidance from the previous assignment if you don't remember how to do it.

### Create two new classes and an interface

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

Implement a function that instantiates 1 000 000 ``Player`` objects with random Guid as the Id.

Write code that checks that there are no duplicate Ids among the ``Player`` objects.

## 2. Extension method

Implement an extension method ``GetHighestLevelItem`` for the ``Player`` which returns the ``Item`` with highest level from the list.

Write some runnable code that uses the method.

## 3. LINQ

Create two variations of a function that takes a ``Player`` as a parameter and returns all ``Item`` objects in the list as an array.

- ``GetItems``: Transform the list to an array using normal C# loop to do the work and return it
- ``GetItemsWithLinq``: Transform the list to an array by using LINQ extension methods and return it

Write some runnable code that uses the functions.

## 4. LINQ 2

Create two variations of a function that takes a ``Player`` as a parameter and returns the first ``Item`` object in the ``Item``-list owned by the player.

- ``FirstItem``: Get the first item from a list and return it. If the list is empty, return null.
- ``FirstItemWithLinq``: Find the LINQ extension method that does the same thing. (Here you can find the available methods for collections: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netcore-2.2)

Write some runnable code that uses the functions.

## 5. Delegates

Implement a function with signature ``ProcessEachItem(Player player, Action<Item> process);``. This function should call the delegate on each of the ``Item`` objects owned by the ``Player`` object.

Write code that uses the ``ProcessEachItem`` function to print the ``Id`` and ``Level`` of each item to the console. The code should pass an another function with signature ``PrintItem(Item item)`` to the function as the second parameter.

## 6. Lambda

Call the ``ProcessEachItem`` function (implemented in the previous exercise) with a lambda function that does the same thing as the function in the previous excercise (print the ``Id`` and ``Level`` to the console).

## 7. Generics

Create a generic class called ``Game``:

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

Implement the ``GetTop10Players`` method.

Write an another class that implements the IPlayer interface called ``PlayerForAnotherGame``.

Write code that demonstrates that you can instantiate the generic ``Game`` class and call ``GetTop10Players`` with both ``Player`` and ``PlayerForAnotherGame``.
