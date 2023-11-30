using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoScrapper.DAL.Interfaces
{
    public interface IMongoRepository<T>
    {
        void InsertDocument(T document);
        T GetDocument(FilterDefinition<T> filter);
        long UpdateDocument(FilterDefinition<T> filter, UpdateDefinition<T> update);
        long DeleteDocument(FilterDefinition<T> filter);
        IEnumerable<T> GetAllDocuments(FilterDefinition<T> filter);
    }
}
