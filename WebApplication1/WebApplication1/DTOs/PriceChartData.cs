using System.Text.Json.Serialization;

namespace WebApplication1.DTOs;

public class PriceChartData
{
    public DateTime x => new(t);

    [JsonPropertyName("t")] public long t { get; set; }
    [JsonPropertyName("o")] public double open { get; set; }
    [JsonPropertyName("l")] public double low { get; set; }
    [JsonPropertyName("c")] public double close { get; set; }
    [JsonPropertyName("h")] public double high { get; set; }
    [JsonPropertyName("v")] public double volume { get; set; }
}