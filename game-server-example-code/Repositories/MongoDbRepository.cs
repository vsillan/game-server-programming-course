using System.Collections.Immutable;
using System.Linq;
using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using game_server.Players;

namespace game_server.Repositories
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _collection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Game");
            _collection = database.GetCollection<Player>("players");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player[]> GetAllPlayers()
        {
            var players = await _collection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Player> GetPlayer(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            return _collection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetBetweenLevelsAsync(int minLevel, int maxLevel)
        {
            var filter = Builders<Player>.Filter.Gte(p => p.Level, 18) & Builders<Player>.Filter.Lte(p => p.Level, 30);
            var players = await _collection.Find(filter).ToListAsync();
            return players.ToArray();
        }


        public Task<Player> IncreasePlayerScoreAndRemoveItem(Guid playerId, Guid itemId, int score)
        {
            var pull = Builders<Player>.Update.PullFilter(p => p.Items, i => i.Id == itemId);
            var inc = Builders<Player>.Update.Inc(p => p.Score, score);
            var update = Builders<Player>.Update.Combine(pull, inc);
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);

            return _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player[]> GetAllSortedByScoreDescending()
        {
            var sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            var players = await _collection.Find(new BsonDocument()).Sort(sortDef).ToListAsync();

            return players.ToArray();
        }

        public async Task<Player> IncrementPlayerScore(string id, int increment)
        {
            var filter = Builders<Player>.Filter.Eq("_id", id);
            var incrementScoreUpdate = Builders<Player>.Update.Inc(p => p.Score, increment);
            var options = new FindOneAndUpdateOptions<Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
            Player player = await _collection.FindOneAndUpdateAsync(filter, incrementScoreUpdate, options);
            return player;
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            return await _collection.FindOneAndDeleteAsync(filter);
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
