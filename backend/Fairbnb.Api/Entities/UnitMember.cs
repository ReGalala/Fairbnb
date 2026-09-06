using Microsoft.AspNetCore.Identity;
namespace Fairbnb.Api.Entities;

public class UnitMember
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Role { get; set; }= "User";
    public bool IsActive { get; set;} = true;
    public DateTime JoinedAt {get; set;} = DateTime.UtcNow;

    public Unit Unit { get; set;} = null!;
    public IdentityUser User { get; set; } = null!;

}