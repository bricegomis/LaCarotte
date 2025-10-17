using Ati.API.Common.Models.Documents;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LaCarotte.API.Models
{
    public class Profile : ProfileDocument
    {
        public required string Login { get; set; }
        public double ScoreTotal { get; set; }
        public double ScoreWeek { get; set; }
        public double ScoreDay { get; set; }
        public List<string> CarotteIds { get; set; } = [];
        public List<Carotte>? Carotte { get; set; }
    }
}
