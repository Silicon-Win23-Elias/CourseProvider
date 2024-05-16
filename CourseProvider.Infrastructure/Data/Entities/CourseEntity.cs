using System.ComponentModel.DataAnnotations;

namespace CourseProvider.Infrastructure.Data.Entities;

public class CourseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsDigital { get; set; }
    public string[]? Categories { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public decimal StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? LikesPercent { get; set; }
    public string? Likes { get; set; }
    public string? Duration { get; set; }
    public virtual List<AuthorEntity>? Authors { get; set; }
    public virtual PricesEntity? Prices { get; set; }
    public virtual ContentEntity? Content { get; set; }
}
