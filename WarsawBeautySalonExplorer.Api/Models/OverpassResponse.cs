using System.Text.Json.Serialization;

namespace WarsawBeautySalonExplorer.Api.Models;

public class OverpassResponse
{
    [JsonPropertyName("elements")]
    public List<OverpassElement> Elements { get; set; } = [];
}