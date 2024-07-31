using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace apimongodblucca.Domains
{
    public class Product
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("produto")]
        public string? Name { get; set; }

        [BsonElement("preco")]
        public decimal? Price { get; set; }

        public Dictionary<string,string>? AdditionalAtributes { get; set; }

        public Product()
        {
            AdditionalAtributes = new Dictionary<string,string>();
        }
    }
}
