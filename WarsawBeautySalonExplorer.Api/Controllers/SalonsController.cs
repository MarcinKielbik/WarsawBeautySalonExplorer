using Microsoft.AspNetCore.Mvc;
using WarsawBeautySalonExplorer.Api.DTOs;
using WarsawBeautySalonExplorer.Api.Services;

namespace WarsawBeautySalonExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalonsController : ControllerBase
{
    private readonly ISalonService _salonService;
    private readonly SalonImportService _salonImportService;

    private readonly SeedImportService _seedImportService;

    public SalonsController(ISalonService salonService,
    SalonImportService salonImportService,
    SeedImportService seedImportService)
    {
        _salonService = salonService;
        _salonImportService = salonImportService;
        _seedImportService = seedImportService;

    }


    [HttpGet]
    public async Task<ActionResult<List<SalonDetailsDto>>> GetAll(
    [FromQuery] string? district,
    [FromQuery] string? service)
    {
        var salons = await _salonService.GetAllAsync(district, service);

        return Ok(salons);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<SalonDetailsDto>> GetById(int id)
    {
        var salon = await _salonService.GetByIdAsync(id);

        if (salon is null)
        {
            return NotFound();
        }

        return Ok(salon);
    }


    [HttpPost]
    public async Task<IActionResult> AddSalon([FromBody] SalonDto salonDto)
    {
        var createdSalon = await _salonService.AddAsync(salonDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdSalon.Id },
            createdSalon
        );

    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalonDto salonDto)
    {
        var updated = await _salonService.UpdateAsync(id, salonDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _salonService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }



    [HttpPost("import/osm")]
    public async Task<IActionResult> ImportFromOpenStreetMap()
    {
        var importedCount = await _salonImportService.ImportFromOpenStreetMapAsync();

        return Ok(new
        {
            importedCount
        });
    }

    [HttpPost("import/seed")]
    public async Task<IActionResult> ImportFromSeed()
    {
        var importedCount = await _seedImportService.ImportAsync();

        return Ok(new
        {
            importedCount
        });
    }
}