using System.Security.Claims;
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var unit = await _unitsService.CreateUnitAsync(request, userId);
        return CreatedAtAction(nameof(GetAll), new {id = unit.Id}, unit);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId= User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var units = await _unitsService.GetAllUnitsAsync(userId);
        return Ok(units);
    }
}