using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public class PolygonService : IPolygonService
{
    private readonly string API_KEY;
    private readonly int SEARCH_COUNT_LIMIT = 10;

    public PolygonService(IConfiguration configuration)
    {
        API_KEY = configuration["POLYGON_API_KEY"];
    }

    public async Task<List<SearchCompanyDto>?> SearchCompany(string ticker)
    {
        using var httpClient = new HttpClient();
        var uriBuilder = new UriBuilder(IPolygonService.BASE_URL + "/v3/reference/tickers");
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["search"] = ticker;
        query["active"] = "true";
        query["sort"] = "ticker";
        query["order"] = "asc";
        query["limit"] = SEARCH_COUNT_LIMIT.ToString();
        query["apiKey"] = API_KEY;

        uriBuilder.Query = query.ToString();
        var response = await httpClient.GetStreamAsync(uriBuilder.ToString());
        var searchResult = await JsonSerializer.DeserializeAsync<ResponseDto<List<SearchCompanyDto>?>>(response);

        return searchResult?.Results;
    }

    private class ResponseDto<T>
    {
        [JsonPropertyName("results")] public T Results { get; set; }
    }
}