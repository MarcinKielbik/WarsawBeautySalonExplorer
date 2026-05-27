using System.Text.Json.Serialization;

namespace WarsawBeautySalonExplorer.Api.Models;

public class OverpassTags
{

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }

    [JsonPropertyName("shop")]
    public string? Shop { get; set; }

    [JsonPropertyName("beauty")]
    public string? Beauty { get; set; }

    [JsonPropertyName("addr:street")]
    public string? Street { get; set; }

    [JsonPropertyName("addr:housenumber")]
    public string? HouseNumber { get; set; }
}