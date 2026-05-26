using WarsawBeautySalonExplorer.Api.Models;

namespace WarsawBeautySalonExplorer.Api.Repositories;

public interface ISalonRepository
{
    Task<List<Salon>> GetAllAsync(string? district, string? service);
    Task<Salon?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task UpdateAsync(Salon salon);
}