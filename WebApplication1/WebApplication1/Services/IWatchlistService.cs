using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IWatchlistService
{
    Task<IEnumerable<CompanyDto>> GetCompanies(string login, int page, int size);
    Task<string> AddCompany(string login, string ticker);
    Task<string> DeleteCompany(string login, string ticker);
    Task<bool> HasUserCompany(string login, string ticker);
}