using System.Text.Json.Serialization;

namespace WarsawBeautySalonExplorer.Api.Models;

public class OverpassElement
{
    [JsonPropertyName("tags")]
    public OverpassTags? Tags { get; set; }
}