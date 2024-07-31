using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apimongodblucca.Domains
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string? Name { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("senha")]
        public string? Password { get; set; }
        public Dictionary<string, string>? AdditionalAtributes { get; set; }

        public User()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }
    }
}
