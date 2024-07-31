using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MinimalAPIMongoDB.Domains
{
    public class Order
    {

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Date")]
        public DateOnly Date { get; set; }

        [BsonElement("Status")]
        public bool? Status { get; set; }

        [BsonElement("ProductsIds")]
        [JsonIgnore]
        public List<string>? ProductId { get; set; }

        
        [BsonElement("Product")]
        public List<Product>? Products { get; set; }
        [BsonElement("ClientId")]
        public string? ClientId { get; set; }

        [BsonElement("Client")]
        public Client? Client { get; set; }
    }
}
