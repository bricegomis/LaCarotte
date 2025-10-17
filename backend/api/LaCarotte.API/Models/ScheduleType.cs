using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LaCarotte.API.Models
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
