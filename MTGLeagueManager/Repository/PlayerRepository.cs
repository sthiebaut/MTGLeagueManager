using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MTGLeagueManager.ReadModel;

namespace MTGLeagueManager.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private IMongoCollection<Player> GetCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("mtgleaguemanager");
            var collection = database.GetCollection<Player>("players");

            return collection;
        }

        public async Task<List<Player>> LoadAllAsync()
        {
            var collection = GetCollection();
            var filter = new BsonDocument();
            var result = collection.Find(filter).ToListAsync();
            return await result;
        } 

        public async Task<Player> GetByIdAsync(Guid id)
        {
            var collection = GetCollection();
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var players = await collection.Find(filter).ToListAsync();

            return players != null && players.Any() ? players.First() : null;
        }

        public Task AddAsync(Player player)
        {
            var collection = GetCollection();
            return collection.InsertOneAsync(player);
        }

        public Task RemoveAsync(Guid id)
        {
            var collection = GetCollection();
            var filter = Builders<Player>.Filter.Eq("Id", id);
            return collection.FindOneAndDeleteAsync(filter);
        }

        public Task RenameAsync(Guid id, string newName)
        {
            var collection = GetCollection();
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var update = Builders<Player>.Update
                            .Set("Name", newName)
                            .CurrentDate("lastModified");
            return collection.FindOneAndUpdateAsync(filter, update);
        }
    }
}
