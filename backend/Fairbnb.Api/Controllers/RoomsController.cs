using System.Security.Claims;
using Fairbnb.Api.DTOs;
using Fairbnb.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fairbnb.Api.Controllers;

[ApiController]
[Route("api/units/{unitId}/rooms")]
[Authorize]

public class RoomsController: ControllerBase
{
    private readonly RoomsService _roomsService;

    public RoomsController(RoomsService roomsService)
    {
        _roomsService = roomsService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateRoom(int unitId, CreateRoomRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isMember = await _roomsService.IsUserMemberAsync(unitId, userId);
        if(!isMember)
            return Forbid();
        var room = await _roomsService.CreateRoomAsync(unitId, request);
        return CreatedAtAction(nameof(GetAllRoomsByUnit), new{ unitId, id = room.Id}, room);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoomsByUnit(int unitId){
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isMember = await _roomsService.IsUserMemberAsync(unitId, userId);
        if(!isMember)
            return Forbid();
        var rooms = await _roomsService.GetRoomsByUnitAsync(unitId);
        return Ok(rooms);
    }
}