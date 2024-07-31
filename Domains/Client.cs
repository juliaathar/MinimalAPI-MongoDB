using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MinimalAPIMongoDB.Domains
{
    public class Client
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("CPF")]
        public string Cpf { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Addres")]
        public string Address { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("User")]
        public User? user { get; set; }

        public Dictionary<string, string> AdditionalAttributes { get; set; }
        public Client()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
