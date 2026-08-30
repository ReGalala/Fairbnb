using Fairbnb.Api.DTOs;
using Fairbnb.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fairbnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UnitsController: ControllerBase
{
    private readonly UnitsService _unitsService;

    public UnitsController(UnitsService unitsService)
    {
        _unitsService = unitsService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUnitRequest request)
    {
        var unit = await _unitsService.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), new {id = unit.Id}, unit);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var units = await _unitsService.GetAllAsync();
        return Ok(units);
    }
}