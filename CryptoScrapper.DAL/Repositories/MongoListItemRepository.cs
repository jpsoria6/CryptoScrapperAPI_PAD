using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CryptoScrapper.DAL
{

    public class MongoListItemRepository<CustomListItem> : IMongoRepository<CustomListItem>
    {
        private IMongoCollection<CustomListItem> _collectionList;

        public MongoListItemRepository(IOptions<ListItemDatabaseSettings> listDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            listDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                listDatabaseSettings.Value.DatabaseName);

            _collectionList = mongoDatabase.GetCollection<CustomListItem>(
                listDatabaseSettings.Value.CollectionListItemName);

        }

        public void InsertDocument(CustomListItem document)
        {
            _collectionList.InsertOne(document);
        }

        public CustomListItem GetDocument(FilterDefinition<CustomListItem> filter)
        {
            return _collectionList.Find(filter).FirstOrDefault();
        }

        public long UpdateDocument(FilterDefinition<CustomListItem>   filter, UpdateDefinition<CustomListItem> update)
        {   
            var result = _collectionList.UpdateOne(filter, update);
            return result.ModifiedCount;
        }

        public long DeleteDocument(FilterDefinition<CustomListItem> filter)
        {
            var result = _collectionList.DeleteOne(filter);
            return result.DeletedCount;
        }

        public IEnumerable<CustomListItem> GetAllDocuments(FilterDefinition<CustomListItem> filter)
        {
            return _collectionList.Find(filter).ToList<CustomListItem>();
        }
    }

}