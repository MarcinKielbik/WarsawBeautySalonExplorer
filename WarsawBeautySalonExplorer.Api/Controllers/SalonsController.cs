using Microsoft.AspNetCore.Mvc;
using WarsawBeautySalonExplorer.Api.DTOs;
using WarsawBeautySalonExplorer.Api.Services;

namespace WarsawBeautySalonExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalonsController : ControllerBase
{
    private readonly ISalonService _salonService;

    public SalonsController(ISalonService salonService)
    {
        _salonService = salonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SalonListDto>>> GetAll(
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSalonDto dto)
    {
        var updated = await _salonService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
}