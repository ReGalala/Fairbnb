using Fairbnb.Api.Data;
using Fairbnb.Api.DTOs;
using Fairbnb.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fairbnb.Api.Services;

public class RoomsService
{
    private readonly AppDbContext _context;

    public RoomsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoomResponse> CreateRoomAsync(int unitId, CreateRoomRequest request)
    {
        var room = new Room
        {
            UnitId = unitId,
            Name = request.Name,
            Capacity = request.Capacity,
            Notes = request.Notes,
            IsActive = true
        };
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return MapToResponse(room);
    }

    public async Task<List<RoomResponse>>GetRoomsByUnitAsync(int unitId)
    {
        var rooms = await _context.Rooms
            .Where(room => room.UnitId == unitId)
            .ToListAsync();
        
        return rooms.Select(MapToResponse).ToList();
    }

    private static RoomResponse MapToResponse(Room room)
    {
        return new RoomResponse
        {
            Id = room.Id,
            UnitId = room.UnitId,
            Name = room.Name,
            Capacity = room.Capacity,
            Notes = room.Notes,
            IsActive = room.IsActive
        };
    }
    public async Task<bool> IsUserMemberAsync(int unitId, string userId)
    {
        return await _context.UnitMembers
          .AnyAsync(member => member.UnitId == unitId && member.UserId == userId && member.IsActive);
    }
}