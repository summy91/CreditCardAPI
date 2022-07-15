using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CreditCardAPI.Data.Entities
{
    public class CreditCardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int AvailableLimit { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}