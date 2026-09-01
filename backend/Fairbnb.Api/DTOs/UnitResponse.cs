namespace Fairbnb.Api.DTOs;

public class UnitResponse
{
    public int Id { get; set;}
    public string Name { get; set;} = string.Empty;
    public string Address { get; set;} = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set;}
}