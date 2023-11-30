using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using CryptoScrapper.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CryptoScrapper.DAL.Repositories
{

    public class MongoUserRepository<User> : IMongoRepository<User>
    {
        private IMongoCollection<User> _collection;

        public MongoUserRepository(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            userDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<User>(
                userDatabaseSettings.Value.CollectionName);
        }

        public void InsertDocument(User document)
        {
            _collection.InsertOne(document);
        }

        public User GetDocument(FilterDefinition<User> filter)
        {
            return _collection.Find(filter).FirstOrDefault();
        }

        public long UpdateDocument(FilterDefinition<User> filter, UpdateDefinition<User> update)
        {
            var result = _collection.UpdateOne(filter, update);
            return result.ModifiedCount;
        }

        public long DeleteDocument(FilterDefinition<User> filter)
        {
            var result = _collection.DeleteOne(filter);
            return result.DeletedCount;
        }

        public IEnumerable<User> GetAllDocuments(FilterDefinition<User> filter)
        {
            return _collection.Find(filter).ToList<User>();
        }
    }

}