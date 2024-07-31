using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apimongodblucca.Domains
{
    public class Client
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userid")] 
        public string? UserId { get; set; }

        [BsonElement("cpf")]
        public string? Cpf { get; set; }

        [BsonElement("numero")]
        public string? Number { get; set; }

        [BsonElement("endereco")]
        public string? Address { get; set; }

        public Dictionary<string, string>? AdditionalAtributes { get; set; }

        public Client()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }
    }
}
