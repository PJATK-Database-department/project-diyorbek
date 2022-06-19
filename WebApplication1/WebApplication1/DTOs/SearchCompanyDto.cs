using System.Text.Json.Serialization;

namespace WebApplication1.DTOs;

public class SearchCompanyDto
{
    [JsonPropertyName("ticker")] public string Ticker { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }
}