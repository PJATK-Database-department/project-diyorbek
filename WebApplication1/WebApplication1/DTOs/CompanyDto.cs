using System.Text.Json.Serialization;

namespace WebApplication1.DTOs;

public class CompanyDto
{
    [JsonPropertyName("address")] public Address Address { get; set; }
    [JsonPropertyName("branding")] public Branding Branding { get; set; }
    [JsonPropertyName("description")] public string Descripton { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("phone_number")] public string PhoneNumber { get; set; }
    [JsonPropertyName("ticker")] public string Ticker { get; set; }
    [JsonPropertyName("total_employees")] public int TotalEmployees { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
}

public class Address
{
    [JsonPropertyName("address1")] public string Address1 { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }
    [JsonPropertyName("state")] public string State { get; set; }
}

public class Branding
{
    [JsonPropertyName("icon_url")] public string IconUrl { get; set; }
    [JsonPropertyName("logo_url")] public string LogoUrl { get; set; }
}