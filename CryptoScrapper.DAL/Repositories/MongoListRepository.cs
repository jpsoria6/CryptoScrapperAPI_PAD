using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using CryptoScrapper.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CryptoScrapper.DAL.Repositories
{

    public class MongoListRepository<CustomList> : IMongoRepository<CustomList>
    {
        private IMongoCollection<CustomList> _collectionList;

        public MongoListRepository(IOptions<ListDatabaseSettings> listDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            listDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                listDatabaseSettings.Value.DatabaseName);

            _collectionList = mongoDatabase.GetCollection<CustomList>(
                listDatabaseSettings.Value.CollectionListName);

        }

        public void InsertDocument(CustomList document)
        {
            _collectionList.InsertOne(document);
        }

        public CustomList GetDocument(FilterDefinition<CustomList> filter)
        {
            return _collectionList.Find(filter).FirstOrDefault();
        }

        public long UpdateDocument(FilterDefinition<CustomList> filter, UpdateDefinition<CustomList> update)
        {
            var result = _collectionList.UpdateOne(filter, update);
            return result.ModifiedCount;
        }

        public long DeleteDocument(FilterDefinition<CustomList> filter)
        {
            var result = _collectionList.DeleteOne(filter);
            return result.DeletedCount;
        }

        public IEnumerable<CustomList> GetAllDocuments(FilterDefinition<CustomList> filter)
        {
            return _collectionList.Find(filter).ToList<CustomList>();
        }
    }

}