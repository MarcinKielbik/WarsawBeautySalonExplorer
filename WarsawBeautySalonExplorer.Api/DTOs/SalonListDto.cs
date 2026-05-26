namespace WarsawBeautySalonExplorer.Api.DTOs;

public class SalonListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;
    public string? PriceRange { get; set; }

    public double? Rating { get; set; }
}