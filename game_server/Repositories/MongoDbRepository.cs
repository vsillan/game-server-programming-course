using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
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
            IMongoDatabase database = mongoClient.GetDatabase("Game");
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
            List<Player> players = await _collection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public Task<Player> GetPlayer(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("_id", id);
            return _collection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetBetweenLevelsAsync(int minLevel, int maxLevel)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Level", 18) & Builders<Player>.Filter.Lte("Level", 30);
            List<Player> players = await _collection.Find(filter).ToListAsync();
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
            var filter = Builders<Player>.Filter.Eq("_id", player.Id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player[]> GetAllSortedByScoreDescending()
        {
            SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending(p => p.Score);
            List<Player> players = await _collection.Find(new BsonDocument()).Sort(sortDef).ToListAsync();
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

        public Task<Player> DeletePlayer(Guid playerId)
        {
            throw new NotImplementedException();
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
