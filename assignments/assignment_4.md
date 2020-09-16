# Assignment 4

_This assignment builds on top of the `GameWebApi` project created in assignment 3._

Following exercises will help you get more experience in using ASP.NET Core Web API with the addition of doing server-side validation for the data that the client is sending.

In the real world, it is very important to get the validation on server-side right. This is true especially for competetive multiplayer games. The validation is done to make sure that players can't cheat in the game by sending illegal data.

---

## 1. Implement routes (CREATE/READ/UPDATE/DELETE) and data classes for `Item`

Implement routes and data classes for `Item`.

### Tips for the implementation:

You need at least the following new classes:

`Item`, `NewItem`, `ModifiedItem`, `ItemsController`. Also, you need to add more methods to the `IRepository` interface and implement them in the `FileRepository`. Now your `IRepository` interface should look like this:

```C#
public interface IRepository
{
    Task<Player> CreatePlayer(Player player);
    Task<Player> GetPlayer(Guid playerId);
    Task<Player[]> GetAllPlayers();
    Task<Player> UpdatePlayer(Player player);
    Task<Player> DeletePlayer(Guid playerId;

    Task<Item> CreateItem(Guid playerId, Item item);
    Task<Item> GetItem(Guid playerId, Guid itemId);
    Task<Item[]> GetAllItems(Guid playerId);
    Task<Item> UpdateItem(Guid playerId, Item item);
    Task<Item> DeleteItem(Guid playerId, Item item);
}
```

Items should be owned by the players which means that we want to add a list of items (List<Item>) to the player class.

All item routes should start with `.../api/players/{playerId}/items`.

---

## 2. MongoDb

The purpose of this exercise is to learn to write code that accesses data in MongoDb database. You will create a class `MongoDbRepository` which has the responbility to do everything that is related accessing data in MongoDb. It will replace your existing `FileRepository`.

`MongoDbRepository` should also implement the `IRepository` interface - just like the `FileRepository` does.

When it's time to run your application with MongoDb, remember to replace the `FileRepository` registeration with the new `MongoDbRepository` in the DI-Container! (in `Startup.cs`)

### Tips for the implementation:

To get the MongoDb driver installed, run the following command in the project folder: `dotnet add package MongoDb.Driver`.

You need to create a connection to the MongoDb that should be running on your local development machine. If the MongoDb is running with default port, this should work as a connection string: `mongodb://localhost:27017`.

Look at the example code in this repository to get hints on how to use MongoDb with C#.

Your data should follow this format:

- You can name your database to `game`
- Players should be stored in a collection called `players`
- `Items` should be stored in a list inside the `Player` document

---

## 3. Error handling

Create your own middleware for handling errors called `ErrorHandlingMiddleware`.

Create your own exception class called `NotFoundException`.

Throw the `NotFoundException` when `Player` is not found (incorrect {playerId} passed in the route) when trying to add new `Item`.

Catch the `NotFoundException` in the `ErrorHandlingMiddleware`. And then on the catch block: set the HTTP status code to 404 (not found) to the `HttpContext` that is passed to the middleware.

---

## 4. Model validation using attributes

`NewItem` and `Item` models should have the following properties:

- int Level
- ItemType Type (define the `ItemType` enum yourself with values SWORD, POTION and SHIELD)
- DateTime CreationDate

Define the following validations for the model using attributes:

- "Level can be only within the range from 1 to 99
- "Type" is one of the types defined in the `ItemType` enum
- "CreationDate" is a date from the past (Create custom validation attribute)

---

## 5 (EXTRA!). Implement a game rule validation in Controller

This is an extra exercise. You will get bonus points for completing this.

Implement a game rule validation for the `[POST]` (the one that creates a new item) route in the `ItemsContoller`:

The rule should be: an item of type of `Sword` should not be allowed for a `Player` below level 3.

If the rule is not followed, throw your own custom exception (create the exception class) and catch the exception in an `exception filter`. The `exception filter` should write a response to the client with a _suitable error code_ and a _descriptive error message_. The `exception filter` should be only applied to that specific route.

---
