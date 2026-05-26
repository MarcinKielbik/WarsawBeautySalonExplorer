namespace WarsawBeautySalonExplorer.Api.DTOs;

public class SalonDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Services { get; set; }
    public string? PriceRange { get; set; }

    public double? Rating { get; set; }
    public int? ReviewCount { get; set; }
}

