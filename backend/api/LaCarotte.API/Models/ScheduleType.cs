using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace carotte.API.Models
{
    public enum ScheduleType
    {
        [BsonRepresentation(BsonType.String)]
        None,
        [BsonRepresentation(BsonType.String)]
        Daily,
        [BsonRepresentation(BsonType.String)]
        Weekly,
        [BsonRepresentation(BsonType.String)]
        Monthly,
    }
}
