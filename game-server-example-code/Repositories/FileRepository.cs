using System;
using System.Threading.Tasks;
using game_server.Players;
using game_server.Repositories;

public class FileRepository : IRepository
{
    public Task<LevelCount[]> GetLevelCounts()
    {
        throw new NotImplementedException();
    }

    public Task<Item> CreateItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> CreatePlayer(Player player)
    {
        throw new NotImplementedException();
    }

    public Task<Item> DeleteItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> DeletePlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Item[]> GetAllItems(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Player[]> GetAllPlayers()
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<Player> GetPlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Item> UpdateItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> UpdatePlayer(Player player)
    {
        throw new NotImplementedException();
    }
}