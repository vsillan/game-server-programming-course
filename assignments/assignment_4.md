# Assignment 4

The purpose of this assignment is to learn to write code that accesses data in MongoDb database. You will create a class ``MongoDbRepository`` which has the responbility to do everything that is related accessing data in MongoDb. It will replace your existing ``InMemoryRepository``.

``MongoDbRepository`` should also implement the ``IRepository`` interface - just like the ``InMemoryRepository`` does.

Currently your ``IRepository`` interface should look roughly like this:

```
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

When it's time to run your application with MongoDb, remember to replace the ``InMemoryRepository`` registeration with the new ``MongoDbRepository`` in the DI-Container! (``Startup.cs``)

## MongoDb implementation

You need to create a connection to the MongoDb that should be running on your local development machine. If the MongoDb is running with default port, this should work as a connection string: ``mongodb://localhost:27017``.

Your data should follow this format:

- You can name your database to ``game``
- Players should be stored in a collection called ``players``
- ``Items`` should be stored in a list inside ``Player`` model

---