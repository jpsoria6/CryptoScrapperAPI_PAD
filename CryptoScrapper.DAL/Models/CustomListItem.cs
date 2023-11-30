using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoScrapper.DAL.Models
{
    public class CustomListItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }

        #region ListItem References
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomListId { get; set; }

        [BsonIgnore]
        public CustomList List { get; set; }
        #endregion

    }
}
