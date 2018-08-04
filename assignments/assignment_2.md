# Assignment 2

The purpose of the following exercises is to get you familiar with writing API-endpoints using layered architecture. Layered architecture means that we divide the responsibilities related to handling the requests between different classes.

It's generally a bad practice to write the business logic and data access logic inside the controller which receives the API requests. This is why we extract the business logic into a class called ``PlayerProcessor`` and the data access logic into a class called ``InMemoryRepository``.

After the exercises we have implemented CRUD (Create, Read, Update, Delete) operations for the Players API.

**Help**

If you feel that you need more explanation, you can read an implementation example of a TODO-list app from here: https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-vsc?view=aspnetcore-2.1

Please note that the example uses ``Entity Framework`` as the database access technology. That is not something we do in this course so you should skip those parts and use the ``InMemoryRepository`` presented on this assignment.

---

## 1. Create Model classes

Create the following classes.

``Player`` class is used to define objects that are persisted and served to the client.

```
public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
}
```

``NewPlayer`` classs used to define object that contains the properties than are defined by the client when creating new player. ``Id`` and ``CreationDate`` should be set by the server when the player is created.

```
public class NewPlayer
{
    public string Name { get; set; }
}
```

``ModifiedPlayer`` class is used to defined object that contains the properties that can be modified on a player.

```
public class ModifiedPlayer
{
    public int Score { get; set; }
}
```

## 2. Create InMemoryRepository class and IRepository interface

The responsibility of the ``Repository`` is to handle accessing and persisting objects.

Create the following interface:

```
public interface IRepository
{
    public Task<Player> Get(Guid id);
    public Task<Player[]> GetAll();
    public Task<Player> Create(Player player);
    public Task<Player> Modify(Guid id, ModifiedPlayer player);
    public Task<Player> Delete(Guid id);
}
```

Create a class called ``InMemoryRepository`` which implements the interface. Utilize a datastructure of your choice to get, create, modify and delete players in memory.

---

## 3. Create PlayersProcessor class

The responbility of the processor class is to handle the business logic. This can include things such as generating IDs when creating a player and deciding which properties to change when modifying a player.

``PlayersProcessor`` should get ``IRepository`` through dependency injection and use it to do data access operations.

Create a class called ``PlayersProcessor``. Add and implement the following methods:

```
public Task<Player> Get(Guid id);
public Task<Player[]> GetAll();
public Task<Player> Create(NewPlayer player);
public Task<Player> Modify(Guid id, ModifiedPlayer player);
public Task<Player> Delete(Guid id);
```

---

## 4. Create a PlayersController class

The responsibility of the controller is to define the endpoints for the API. Define the routes using attribute routing according to REST-principles.

``PlayersController`` should get ``PlayersProcessor`` through dependency injection and use it to delegate the request processing.

Create a class called ``PlayersController``. Add and implement the following methods:

```
public Task<Player> Get(Guid id);
public Task<Player[]> GetAll();
public Task<Player> Create(NewPlayer player);
public Task<Player> Modify(Guid id, ModifiedPlayer player);
public Task<Player> Delete(Guid id);
```

---

## 5. Register PlayerProcessor and Repository to DI-container

Register ``PlayersProcessor`` and ``InMemoryRepository`` to the DI-container in ``Startup.cs`` - ``ConfigureServices`` using extension methods.

Register the InMemoryRepository as IRepository into the dependency injection container to enable changing the implementation later on when we start using MongoDB as the database.

---

## 6. Test

Use a tool such as PostMan to test that the requests to all endpoints are processed succesfully.
