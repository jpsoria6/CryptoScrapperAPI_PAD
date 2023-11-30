using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoScrapper.DAL.Models
{
    public class CustomList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }


        #region User References
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonIgnore]
        public User User { get; set; }

        #endregion

        #region ListItem References
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ListItemIds { get; set; }

        [BsonIgnore]
        public List<CustomListItem> ListItems { get; set; }
        #endregion
    }
}
