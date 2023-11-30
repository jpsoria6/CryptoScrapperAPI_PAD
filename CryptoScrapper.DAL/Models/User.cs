using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoScrapper.DAL.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        #region CustomLists References
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CustomListIds { get; set; }

        [BsonIgnore]
        public List<CustomList> CustomLists { get; set; }
        #endregion
    }
}
