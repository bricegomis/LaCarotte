namespace LaCarotte.API.Models.Dto;

public class CarotteCreateDto
{
    public required string Title { get; set; }
    public string? Desc { get; set; }
    public bool? IsReward { get; set; }
    public int? Points { get; set; }
    public string? Image { get; set; }
    public List<string>? Tags { get; set; } = [];
}