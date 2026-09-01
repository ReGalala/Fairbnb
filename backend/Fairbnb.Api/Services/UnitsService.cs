using Fairbnb.Api.Data;
using Fairbnb.Api.DTOs;
using Fairbnb.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fairbnb.Api.Services;

public class UnitsService
{
    private readonly AppDbContext _context;
    public UnitsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UnitResponse> CreateAsync(CreateUnitRequest request)
    {
        var unit = new Unit
        {
            Name = request.Name,
            Address = request.Address,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Units.Add(unit);
        await _context.SaveChangesAsync();

        return MapToResponse(unit);
    }

    public async Task<List<UnitResponse>> GetAllAsync()
    {
        var units = await _context.Units.ToListAsync();
        return units.Select(MapToResponse).ToList();
    }
    
    private static UnitResponse MapToResponse(Unit unit)
    {
        return new UnitResponse
        {
            Id = unit.Id,
            Name = unit.Name,
            Address = unit.Address,
            Status = unit.Status,
            CreatedAt = unit.CreatedAt
        };
    }
}