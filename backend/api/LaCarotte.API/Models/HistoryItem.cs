namespace LaCarotte.API.Models
{
    public class HistoryItem
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public Carotte Item { get; set; }
        public double Points { get; set; }
    }
}