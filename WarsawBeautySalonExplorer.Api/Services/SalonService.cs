using WarsawBeautySalonExplorer.Api.DTOs;
using WarsawBeautySalonExplorer.Api.Models;
using WarsawBeautySalonExplorer.Api.Repositories;

namespace WarsawBeautySalonExplorer.Api.Services;

public class SalonService : ISalonService
{
    private readonly ISalonRepository _salonRepository;

    public SalonService(ISalonRepository salonRepository)
    {
        _salonRepository = salonRepository;
    }



    public async Task<List<SalonDetailsDto>> GetAllAsync(string? district, string? service)
    {
        var salons = await _salonRepository.GetAllAsync(district, service);

        return salons
            .Select(MapToDetailsDto)
            .ToList();
    }


    public async Task<SalonDetailsDto?> GetByIdAsync(int id)
    {
        var salon = await _salonRepository.GetByIdAsync(id);

        if (salon is null)
        {
            return null;
        }

        return MapToDetailsDto(salon);
    }

    public async Task<SalonDetailsDto> AddAsync(SalonDto dto)
    {
        var salon = MapToEntity(dto);

        var createdSalon = await _salonRepository.AddAsync(salon);

        return MapToDetailsDto(createdSalon);
    }

    public async Task<bool> UpdateAsync(int id, SalonDto dto)
    {
        var salon = await _salonRepository.GetByIdAsync(id);

        if (salon is null)
        {
            return false;
        }

        UpdateEntityFromDto(salon, dto);

        await _salonRepository.UpdateAsync(salon);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _salonRepository.DeleteAsync(id);
    }

    private static Salon MapToEntity(SalonDto dto)
    {
        return new Salon
        {
            Name = dto.Name,
            Address = dto.Address,
            District = dto.District,
            PhoneNumber = dto.PhoneNumber,
            WebsiteUrl = dto.WebsiteUrl,
            Services = dto.Services,
            PriceRange = dto.PriceRange,
            Rating = dto.Rating,
            ReviewCount = dto.ReviewCount
        };
    }

    private static SalonDto MapToDto(Salon salon)
    {
        return new SalonDto
        {
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

    private static void UpdateEntityFromDto(Salon salon, SalonDto dto)
    {
        salon.Name = dto.Name;
        salon.Address = dto.Address;
        salon.District = dto.District;
        salon.PhoneNumber = dto.PhoneNumber;
        salon.WebsiteUrl = dto.WebsiteUrl;
        salon.Services = dto.Services;
        salon.PriceRange = dto.PriceRange;
        salon.Rating = dto.Rating;
        salon.ReviewCount = dto.ReviewCount;
    }

    private static SalonListDto MapToListDto(Salon salon)
    {
        return new SalonListDto
        {
            Id = salon.Id,
            Name = salon.Name,
            District = salon.District,
            Rating = salon.Rating,
            PriceRange = salon.PriceRange
        };
    }

    private static SalonDetailsDto MapToDetailsDto(Salon salon)
    {
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
}