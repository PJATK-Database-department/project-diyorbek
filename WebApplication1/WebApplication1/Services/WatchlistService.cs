using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class WatchlistService : IWatchlistService
{
    private readonly AppDbContext _context;

    public WatchlistService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CompanyDto>> GetCompanies(string login, int page, int size)
    {
        var companies = await _context.Watchlists
            .Include(x => x.Company)
            .Where(w => w.Login == login)
            .Select(x => x.Company.Info).ToListAsync();

        return companies
            .Select(x => JsonSerializer.Deserialize<ResponseDto<CompanyDto>>(x)!.Results)
            .ToList();
    }

    public async Task<string> AddCompany(string login, string ticker)
    {
        var user = await _context.Users.FindAsync(login);
        var company = await _context.Companies.FindAsync(ticker);

        if (user == null) throw new Exception("User not found");
        if (company == null) throw new Exception("Company not found");

        await _context.Watchlists.AddAsync(new Watchlist {Login = user.Login, Ticker = company.Ticker});
        await _context.SaveChangesAsync();

        return company.Ticker;
    }

    public async Task<string> DeleteCompany(string login, string ticker)
    {
        var watched = await _context.Watchlists
            .Where(w => w.Login == login && w.Ticker == ticker)
            .FirstOrDefaultAsync();

        if (watched == null) throw new Exception("Entry not found");

        _context.Watchlists.Remove(watched);
        await _context.SaveChangesAsync();

        return ticker;
    }

    public async Task<bool> HasUserCompany(string login, string ticker)
    {
        return await _context.Watchlists
            .AnyAsync(w => w.Login == login && w.Ticker == ticker);
    }
}