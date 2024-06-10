using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DoList.API.Models
{
    [BsonIgnoreExtraElements]
    public class DoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Title { get; set; }
        public string? Desc { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        /// <summary>
        /// Points earned for completing this item
        /// Can be increase by bonus
        /// </summary>
        public int? Points { get; set; }
        public bool? IsFinished { get; set; } = false;
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProfileId { get; set; }
        public DateTimeOffset? DateFinished { get; set; }
        public string? Image { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Tags { get; set; } = [];
        #region Schedule properties
        public string? Schedule { get; set; }
        /// <summary>
        /// Number of points to add to the Points when earning a bonus
        /// </summary>
        public List<DateTimeOffset>? History { get; set; } = [];
        public int? HistoryBonus { get; set; }
        #endregion
    }
}