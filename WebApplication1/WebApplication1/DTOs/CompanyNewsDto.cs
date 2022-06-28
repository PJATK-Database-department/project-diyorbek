using System.Text.Json.Serialization;

namespace WebApplication1.DTOs;

public class CompanyNewsDto
{
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("author")] public string? Author { get; set; }
    [JsonPropertyName("published_utc")] public DateTime? PublishDate { get; set; }
    [JsonPropertyName("article_url")] public string? Url { get; set; }
}