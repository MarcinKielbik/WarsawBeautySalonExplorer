using System.Text.Json;
using WarsawBeautySalonExplorer.Api.Models;
using WarsawBeautySalonExplorer.Api.Repositories;

namespace WarsawBeautySalonExplorer.Api.Services;

public class SeedImportService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ISalonRepository _salonRepository;

    public SeedImportService(
        IWebHostEnvironment environment,
        ISalonRepository salonRepository)
    {
        _environment = environment;
        _salonRepository = salonRepository;
    }

    public async Task<int> ImportAsync()
    {
        var filePath = Path.Combine(
            _environment.ContentRootPath,
            "Data",
            "Seed",
            "salons.json"
        );

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Seed file not found.", filePath);
        }

        var json = await File.ReadAllTextAsync(filePath);

        var salons = JsonSerializer.Deserialize<List<Salon>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        if (salons is null)
        {
            return 0;
        }

        var importedCount = 0;

        foreach (var salon in salons)
        {
            await _salonRepository.AddAsync(salon);
            importedCount++;
        }

        return importedCount;
    }
}