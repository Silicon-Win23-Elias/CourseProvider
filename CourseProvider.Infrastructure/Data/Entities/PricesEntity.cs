namespace CourseProvider.Infrastructure.Data.Entities;

public class PricesEntity
{
    public string? Currency { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
