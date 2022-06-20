using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IPolygonService
{
    public const string BASE_URL = "https://api.polygon.io";

    Task<List<SearchCompanyDto>?> SearchCompany(string ticker);
    Task<CompanyDto?> GetCompanyInfo(string ticker);
}