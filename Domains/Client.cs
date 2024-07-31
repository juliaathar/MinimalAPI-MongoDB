using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MinimalAPIMongoDB.Domains
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("CPF")]
        public string Cpf { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("User")]
        public User? User { get; set; }

        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public Client()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
