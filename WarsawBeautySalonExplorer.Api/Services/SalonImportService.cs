using System.Net.Http.Json;
using WarsawBeautySalonExplorer.Api.Models;
using WarsawBeautySalonExplorer.Api.Repositories;

namespace WarsawBeautySalonExplorer.Api.Services;

public class SalonImportService
{
    private readonly HttpClient _httpClient;
    private readonly ISalonRepository _salonRepository;

    private readonly List<(string District, string BBox)> _areas = new()
    {
        ("Śródmieście", "52.215,20.970,52.260,21.050"),
        ("Mokotów", "52.150,20.950,52.215,21.060"),
        ("Wola", "52.220,20.920,52.260,20.990")
    };

    public SalonImportService(HttpClient httpClient, ISalonRepository salonRepository)
    {
        _httpClient = httpClient;
        _salonRepository = salonRepository;

        _httpClient.Timeout = TimeSpan.FromSeconds(60);

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "WarsawBeautySalonExplorer/1.0"
        );

        _httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    }

    public async Task<int> ImportFromOpenStreetMapAsync()
    {
        var importedCount = 0;
        var importedKeys = new HashSet<string>();

        foreach (var area in _areas)
        {
            var query = BuildQuery(area.BBox);

            using var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("data", query)
            });

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsync(
                    "https://overpass.kumi.systems/api/interpreter",
                    content
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request failed for {area.District}: {ex.Message}");
                continue;
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(
                    $"Overpass error for {area.District}: {(int)response.StatusCode} {response.ReasonPhrase}"
                );

                Console.WriteLine(errorBody);
                continue;
            }

            var overpassResponse =
                await response.Content.ReadFromJsonAsync<OverpassResponse>();

            if (overpassResponse is null)
            {
                Console.WriteLine($"No response for {area.District}");
                continue;
            }

            Console.WriteLine($"{area.District}: elements = {overpassResponse.Elements.Count}");

            foreach (var element in overpassResponse.Elements)
            {
                if (element.Tags is null)
                {
                    continue;
                }

                var tags = element.Tags;

                if (string.IsNullOrWhiteSpace(tags.Name))
                {
                    continue;
                }

                var address = BuildAddress(tags);
                var uniqueKey = $"{tags.Name}|{address}".ToLowerInvariant();

                if (!importedKeys.Add(uniqueKey))
                {
                    continue;
                }

                var salon = new Salon
                {
                    Name = tags.Name,
                    Address = address,
                    District = area.District,
                    PhoneNumber = tags.Phone,
                    WebsiteUrl = tags.Website,
                    Services = tags.Beauty ?? tags.Shop,
                    PriceRange = null,
                    Rating = null,
                    ReviewCount = null
                };

                await _salonRepository.AddAsync(salon);
                importedCount++;
            }

            await Task.Delay(500);
        }

        return importedCount;
    }

    private static string BuildQuery(string bbox)
    {
        // return $"""
        // [out:json][timeout:8];
        // (
        //   node["shop"="beauty"]({bbox});
        //   node["shop"="hairdresser"]({bbox});
        // );
        // out tags 20;
        // """;


        return """
    [out:json][timeout:20];
    (
      node["shop"="hairdresser"](52.09,20.85,52.35,21.20);
    );
    out tags 50;
    """;
    }

    private static string BuildAddress(OverpassTags tags)
    {
        if (!string.IsNullOrWhiteSpace(tags.Street)
            && !string.IsNullOrWhiteSpace(tags.HouseNumber))
        {
            return $"{tags.Street} {tags.HouseNumber}";
        }

        if (!string.IsNullOrWhiteSpace(tags.Street))
        {
            return tags.Street;
        }

        return "Brak adresu";
    }
}