using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography.X509Certificates;

namespace MinimalAPIMongoDB.Domains
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string? Name { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }

        public Dictionary<string, string> AdditionalAttributes { get; set; }  

        public Product() {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
