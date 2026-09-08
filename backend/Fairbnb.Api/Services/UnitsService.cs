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

    public async Task<UnitResponse> CreateUnitAsync(CreateUnitRequest request, string userId)
    {
        var unit = new Unit
        {
            Name = request.Name,
            Address = request.Address,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        var membership = new UnitMember
        {
            Unit = unit,
            UserId = userId,
            Role = "Admin",
            IsActive = true,
            JoinedAt = DateTime.UtcNow
        };

        _context.Units.Add(unit);
        _context.UnitMembers.Add(membership);
        await _context.SaveChangesAsync();

        return MapToResponse(unit);
    }

    public async Task<List<UnitResponse>> GetAllUnitsAsync(string userId)
    {
        var units = await _context.UnitMembers
        .Where(member => member.UserId == userId && member.IsActive)
        .Select(member => member.Unit)
        .ToListAsync();

        return units.Select(MapToResponse).ToList();
    }
    
    public async Task<UnitResponse?> UpdateUnitAsync(UpdateUnitRequest request, int unitId)
    {
        var unit = await _context.Units.FindAsync(unitId);
        if(unit == null)
            return null;        
        unit.Name = request.Name;
        unit.Address = request.Address;
        await _context.SaveChangesAsync();
        return MapToResponse(unit);
    }

    public async Task<bool> IsUserAdminAsync(int unitId, string userId)
    {
        return await _context.UnitMembers
        .AnyAsync(member => member.UnitId == unitId
        && member.UserId == userId
        && member.Role == "Admin"
        && member.IsActive);
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