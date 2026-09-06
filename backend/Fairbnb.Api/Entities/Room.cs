using Fairbnb.Api.Entities;

namespace Fairbnb.Api.Entities;

public class Room
{
    public int Id {get; set; }
    public int UnitId {get; set; }
    public string Name { get; set;} = string.Empty;
    public int? Capacity { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsActive { get; set;} = true;

    public Unit Unit {get; set; }= null!;

}