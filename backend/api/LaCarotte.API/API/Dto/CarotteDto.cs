namespace LaCarotte.API.Models.Dto;

public class CarotteDto
{
    public string Id { get; set; }
    public required string Title { get; set; }
    public string? Desc { get; set; }
    public bool? IsReward { get; set; }
    public int? Points { get; set; }
    public string? Image { get; set; }
    public List<string>? Tags { get; set; } = [];
    public List<DateTimeOffset>? History { get; set; } = [];
    public int? HistoryBonus { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateModified { get; set; }
}