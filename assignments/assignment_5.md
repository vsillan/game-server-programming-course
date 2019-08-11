# Assignment 5

_This assignment builds on top of the ``GameWebApi`` project created in assignment 3 and extended in assignment 4._

The purpose of this assignment is to learn to write code that accesses data in MongoDb database. You will create a class ``MongoDbRepository`` which has the responbility to do everything that is related accessing data in MongoDb. It will replace your existing ``FileRepository``.

``MongoDbRepository`` should also implement the ``IRepository`` interface - just like the ``FileRepository`` does.

Currently your ``IRepository`` interface should look roughly like this:

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

When it's time to run your application with MongoDb, remember to replace the ``FileRepository`` registeration with the new ``MongoDbRepository`` in the DI-Container! (in ``Startup.cs``)

## MongoDb implementation

To get the MongoDb driver installed, run the following command in the project folder: ``dotnet add package MongoDb.Driver``.

You need to create a connection to the MongoDb that should be running on your local development machine. If the MongoDb is running with default port, this should work as a connection string: ``mongodb://localhost:27017``.

Look at the example code in this repository to get hints on how to use MongoDb with C#.

Your data should follow this format:

- You can name your database to ``game``
- Players should be stored in a collection called ``players``
- ``Items`` should be stored in a list inside ``Player`` model
