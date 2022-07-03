using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class PolygonService : IPolygonService
{
    private readonly AppDbContext _context;
    private readonly string API_KEY;
    private readonly int SEARCH_COUNT_LIMIT = 10;

    public PolygonService(IConfiguration configuration, AppDbContext context)
    {
        API_KEY = configuration["POLYGON_API_KEY"];
        _context = context;
    }

    public async Task<List<SearchCompanyDto>> SearchCompany(string ticker)
    {
        try
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

            return searchResult?.Results ?? new List<SearchCompanyDto>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return new List<SearchCompanyDto>();
        }
    }

    public async Task<CompanyDto> GetCompanyInfo(string ticker)
    {
        ResponseDto<CompanyDto?>? searchResult = null;
        var company = await _context.Companies.FindAsync(ticker);

        try
        {
            var response = await RequestCompanyInfo(ticker);
            searchResult =
                await JsonSerializer.DeserializeAsync<ResponseDto<CompanyDto?>>(
                    await response.Content.ReadAsStreamAsync());
            var responseString = await response.Content.ReadAsStringAsync();

            if (company == null)
            {
                await _context.Companies.AddAsync(new Company
                {
                    Ticker = ticker,
                    Info = responseString,
                    UpdatedAt = DateTime.Now,
                    Prices = "{}"
                });
            }
            else
            {
                company.Info = responseString;
                company.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
        catch (HttpRequestException e)
        {
            if (company != null)
                searchResult = JsonSerializer.Deserialize<ResponseDto<CompanyDto?>>(company.Info);
        }


        return searchResult?.Results ?? new CompanyDto();
    }

    public async Task<IEnumerable<CompanyNewsDto>> GetCompanyNews(string ticker)
    {
        using var httpClient = new HttpClient();
        var uriBuilder = new UriBuilder(IPolygonService.BASE_URL + "/v2/reference/news");
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["apiKey"] = API_KEY;
        query["ticker"] = ticker;
        query["limit"] = "5";

        uriBuilder.Query = query.ToString();

        var response = await httpClient.GetAsync(uriBuilder.ToString());
        response.EnsureSuccessStatusCode();
        var searchResult =
            await JsonSerializer.DeserializeAsync<ResponseDto<List<CompanyNewsDto>?>>(response.Content.ReadAsStream());

        return searchResult?.Results ?? new List<CompanyNewsDto>();
    }

    public async Task<IEnumerable<PriceChartData>> GetPriceData(string ticker)
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        var todayYearAgo = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
        using var httpClient = new HttpClient();
        var uriBuilder = new UriBuilder(IPolygonService.BASE_URL +
                                        $"/v2/aggs/ticker/{ticker}/range/1/day/{todayYearAgo}/{today}");
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["apiKey"] = API_KEY;
        query["adjusted"] = "true";
        query["sort"] = "asc";
        uriBuilder.Query = query.ToString();
        var response = await httpClient.GetAsync(uriBuilder.ToString());
        response.EnsureSuccessStatusCode();

        var searchResult =
            await JsonSerializer.DeserializeAsync<ResponseDto<List<PriceChartData>?>>(response.Content.ReadAsStream());

        return searchResult?.Results ?? new List<PriceChartData>();
    }

    private async Task<HttpResponseMessage> RequestCompanyInfo(string ticker)
    {
        using var httpClient = new HttpClient();
        var uriBuilder = new UriBuilder(IPolygonService.BASE_URL + "/v3/reference/tickers/" + ticker);
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["apiKey"] = API_KEY;
        uriBuilder.Query = query.ToString();

        var response = await httpClient.GetAsync(uriBuilder.ToString());
        response.EnsureSuccessStatusCode();

        return response;
    }
}

public class ResponseDto<T>
{
    [JsonPropertyName("results")] public T Results { get; set; }
}