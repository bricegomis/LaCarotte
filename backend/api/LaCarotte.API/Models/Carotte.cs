using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace carotte.API.Models
{
    [BsonIgnoreExtraElements]
    public class Carotte
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Title { get; set; }
        public string? Desc { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public int? Points { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProfileId { get; set; }
        public string? Image { get; set; }
        public List<string>? Tags { get; set; } = [];
        public List<DateTimeOffset>? History { get; set; } = [];
        public int? HistoryBonus { get; set; }
    }
}