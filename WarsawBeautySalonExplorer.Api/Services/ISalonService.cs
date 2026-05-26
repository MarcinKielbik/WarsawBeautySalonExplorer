using WarsawBeautySalonExplorer.Api.DTOs;

namespace WarsawBeautySalonExplorer.Api.Services;

public interface ISalonService
{
    Task<List<SalonListDto>> GetAllAsync(string? district, string? service);
    Task<SalonDetailsDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateSalonDto dto);
}