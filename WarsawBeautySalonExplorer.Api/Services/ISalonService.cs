using WarsawBeautySalonExplorer.Api.DTOs;

namespace WarsawBeautySalonExplorer.Api.Services;

public interface ISalonService
{
    Task<List<SalonDetailsDto>> GetAllAsync(string? district, string? service);
    Task<SalonDetailsDto?> GetByIdAsync(int id);
    Task<SalonDetailsDto> AddAsync(SalonDto dto);
    Task<bool> UpdateAsync(int id, SalonDto dto);
    Task<bool> DeleteAsync(int id);
}