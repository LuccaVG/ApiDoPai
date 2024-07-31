using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace apimongodblucca.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("data")]
        public string? Date { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

        //referencia aos produtos do pedido
        [BsonElement("productId")]
        public List<string>? ProductId { get; set; }
        public List<Product>? Products { get; set; }

        //referencia o cliente que esta fazendo o pedido
        [BsonElement("clientId")]
        public string? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
