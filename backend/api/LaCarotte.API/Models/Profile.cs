using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DoList.API.Models
{
    [BsonIgnoreExtraElements]
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Login { get; set; }
        public double ScoreTotal { get; set; }
        public double ScoreWeek { get; set; }
        public List<string> DoItemsIds { get; set; } = [];
        [BsonIgnore]
        public List<DoItem>? DoItems { get; set; }
        public DateTimeOffset DateLastConnection { get; set; }
        public DateTimeOffset DateCreated { get; internal set; }
    }
}
