using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.API.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; }
    }
}
