# Assignment 3

The purpose of the following exercises is to get you familiar with writing API-routes using a layered architecture. Layered architecture means that we divide the responsibilities related to handling the requests between different classes. In our application architecture we have two layers: a controller and a repository.

After the exercises we have implemented CRUD (Create, Read, Update, Delete) operations for the Players API.

---

## Preparation

### Create new Web API project

- Create a folder called `GameWebApi`
- Move to the folder
- Run `dotnet new webapi`
- Go to the newly created `Startup.cs` class. Remove the line that says `app.UseHttpsRedirection();`. This is important because the requests to the web api will fail if you don't do this.

## 1. Create Model classes

Create the following classes in separate files inside the project folder:

`Player` class is used to define objects that are persisted and served to the client.

```C#
public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
}
```

`NewPlayer` class is used to define object that contains the properties than are defined by the client when creating new player. `Id` and `CreationDate` should be set by the server when the player is created.

```C#
public class NewPlayer
{
    public string Name { get; set; }
}
```

`ModifiedPlayer` class contains the properties that can be modified on a player.

```C#
public class ModifiedPlayer
{
    public int Score { get; set; }
}
```

## 2. Create a FileRepository class and IRepository interface

The responsibility of the `Repository` is to handle accessing and persisting objects.

Create the following interface in a new file:

```C#
public interface IRepository
{
    Task<Player> Get(Guid id);
    Task<Player[]> GetAll();
    Task<Player> Create(Player player);
    Task<Player> Modify(Guid id, ModifiedPlayer player);
    Task<Player> Delete(Guid id);
}
```

Create a class called `FileRepository` which implements the interface. The purpose of the class is to persist and manipulate the `Player` objects in a text file. One possible solution is to serialize the players as JSON to the text file. The text file name should be `game-dev.txt`. You can use, for example, `File.ReadAllText` and `File.WriteAllText` methods for the implementation.

You don't need to care about concurrent access to the file at this point.

---

## 3. Create a PlayersController class

The first responsibility of the controller is to define the routes for the API. Define the routes using attribute.

The second responsibility is to handle the business logic. This can include things such as generating IDs when creating a player, and deciding which properties to change when modifying a player.

`PlayersController` should get `IRepository` through dependency injection and use it to for data access.

Create a class called `PlayersController`. Add and implement the following methods:

```C#
public Task<Player> Get(Guid id);
public Task<Player[]> GetAll();
public Task<Player> Create(NewPlayer player);
public Task<Player> Modify(Guid id, ModifiedPlayer player);
public Task<Player> Delete(Guid id);
```

---

## 4. Register IRepository to DI-container

Register `FileRepository` to the DI-container in `Startup.cs` - `ConfigureServices`.

Registering the `FileRepository` as `IRepository` into the dependency injection container enables changing the implementation later on when we start using `MongoDB` as the database.

---

## 5. Test

Use `PostMan` to test that the requests to all routes are processed succesfully.
