using WarsawBeautySalonExplorer.Api.DTOs;
using WarsawBeautySalonExplorer.Api.Repositories;

namespace WarsawBeautySalonExplorer.Api.Services;

public class SalonService : ISalonService
{
    private readonly ISalonRepository _salonRepository;

    public SalonService(ISalonRepository salonRepository)
    {
        _salonRepository = salonRepository;
    }

    public async Task<List<SalonListDto>> GetAllAsync(string? district, string? service)
    {
        var salons = await _salonRepository.GetAllAsync(district, service);

        return salons.Select(s => new SalonListDto
        {
            Id = s.Id,
            Name = s.Name,
            District = s.District,
            Rating = s.Rating,
            PriceRange = s.PriceRange
        }).ToList();
    }

    public async Task<SalonDetailsDto?> GetByIdAsync(int id)
    {
        var salon = await _salonRepository.GetByIdAsync(id);

        if (salon is null)
        {
            return null;
        }

        return new SalonDetailsDto
        {
            Id = salon.Id,
            Name = salon.Name,
            Address = salon.Address,
            District = salon.District,
            PhoneNumber = salon.PhoneNumber,
            WebsiteUrl = salon.WebsiteUrl,
            Services = salon.Services,
            PriceRange = salon.PriceRange,
            Rating = salon.Rating,
            ReviewCount = salon.ReviewCount
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateSalonDto dto)
    {
        var salon = await _salonRepository.GetByIdAsync(id);

        if (salon is null)
        {
            return false;
        }

        salon.Name = dto.Name;
        salon.Address = dto.Address;
        salon.District = dto.District;
        salon.PhoneNumber = dto.PhoneNumber;
        salon.WebsiteUrl = dto.WebsiteUrl;
        salon.Services = dto.Services;
        salon.PriceRange = dto.PriceRange;
        salon.Rating = dto.Rating;
        salon.ReviewCount = dto.ReviewCount;

        await _salonRepository.UpdateAsync(salon);

        return true;
    }
}