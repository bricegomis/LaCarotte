using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LaCarotte.API.Models
{
    public class Profile
    {
        public string? Id { get; set; }
        public required string Login { get; set; }
        public double ScoreTotal { get; set; }
        public double ScoreWeek { get; set; }
        public double ScoreDay { get; set; }
        public List<string> CarotteIds { get; set; } = [];
        public List<Carotte>? Carotte { get; set; }
        public DateTimeOffset DateLastConnection { get; set; }
        public DateTimeOffset DateCreated { get; internal set; }
    }
}
