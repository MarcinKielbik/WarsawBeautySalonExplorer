using Microsoft.EntityFrameworkCore;
using WarsawBeautySalonExplorer.Api.Data;
using WarsawBeautySalonExplorer.Api.Models;

namespace WarsawBeautySalonExplorer.Api.Repositories;

public class SalonRepository : ISalonRepository
{
    private readonly AppDbContext _context;

    public SalonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Salon>> GetAllAsync(string? district, string? service)
    {
        var query = _context.Salons.AsQueryable();

        if (!string.IsNullOrWhiteSpace(district))
        {
            query = query.Where(s => s.District.ToLower() == district.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(service))
        {
            query = query.Where(s =>
                s.Services != null &&
                s.Services.ToLower().Contains(service.ToLower()));
        }

        return await query
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Salon?> GetByIdAsync(int id)
    {
        return await _context.Salons.FindAsync(id);
    }

    // check if record exist 
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Salons.AnyAsync(s => s.Id == id);
    }

    public async Task UpdateAsync(Salon salon)
    {
        _context.Salons.Update(salon);
        await _context.SaveChangesAsync();
    }
}