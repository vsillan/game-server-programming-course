using System;
using System.Threading.Tasks;
using game_server.Players;

namespace game_server.Repositories
{
    public interface IRepository
    {
        Task<Player> CreatePlayer(Player player);
        Task<Player> GetPlayer(string playerId);
        Task<Player[]> GetAllPlayers();
        Task<Player> UpdatePlayer(Player player);
        Task<Player> DeletePlayer(string playerId);
        Task<LevelCount[]> GetLevelCounts();

        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);
    }
}