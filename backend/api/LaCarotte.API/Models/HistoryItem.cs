namespace LaCarotte.API.Models
{
    public class HistoryItem
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public required Carotte Item { get; set; }
        public double Points { get; set; }
        public string? ProfileId { get; set; }
    }
}