using WarsawBeautySalonExplorer.Api.Models;

namespace WarsawBeautySalonExplorer.Api.Repositories;

public interface ISalonRepository
{
    Task<List<Salon>> GetAllAsync(string? district, string? service);
    Task<Salon?> GetByIdAsync(int id);
    Task<Salon> AddAsync(Salon salon);
    Task UpdateAsync(Salon salon);
    
    Task<bool> DeleteAsync(int id);
}