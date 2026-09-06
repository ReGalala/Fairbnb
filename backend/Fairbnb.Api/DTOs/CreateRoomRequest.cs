namespace Fairbnb.Api.DTOs;

public class CreateRoomRequest
{
    public string Name {get; set; } = string.Empty;
    public int? Capacity {get; set; }
    public string Notes {get; set; } = string.Empty;
}
