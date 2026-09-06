using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace Fairbnb.Api.Tests;

public class RoomsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RoomsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<string> GetTokenAsync()
    {
        var email = $"rooms-{Guid.NewGuid()}@test.com";
        var response = await _client.PostAsJsonAsync("/api/auth/register",
            new { email, password = "Test123!" });
        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return body!.Token;
    }

    private async Task<int> CreateUnitAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/units",
            new { name = "Test Unit", address = "123 Test St" });
        var unit = await response.Content.ReadFromJsonAsync<UnitResponse>();
        return unit!.Id;
    }

    [Fact]
    public async Task CreateRoom_ReturnsCreated()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var unitId = await CreateUnitAsync();

        var response = await _client.PostAsJsonAsync($"/api/units/{unitId}/rooms",
            new { name = "Master Bedroom", capacity = 2, notes = "Main room" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();
        Assert.NotNull(room);
        Assert.Equal("Master Bedroom", room.Name);
        Assert.Equal(unitId, room.UnitId);
    }

    [Fact]
    public async Task GetRooms_ReturnsRoomsForUnit()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var unitId = await CreateUnitAsync();

        await _client.PostAsJsonAsync($"/api/units/{unitId}/rooms",
            new { name = "Living Room" });

        var response = await _client.GetAsync($"/api/units/{unitId}/rooms");
        response.EnsureSuccessStatusCode();

        var rooms = await response.Content.ReadFromJsonAsync<List<RoomResponse>>();
        Assert.NotNull(rooms);
        Assert.Contains(rooms, r => r.Name == "Living Room");
    }

    [Fact]
    public async Task GetRooms_DoesNotReturnRoomsFromOtherUnits()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var unit1Id = await CreateUnitAsync();
        var unit2Id = await CreateUnitAsync();

        await _client.PostAsJsonAsync($"/api/units/{unit1Id}/rooms",
            new { name = "Unit1 Room" });
        await _client.PostAsJsonAsync($"/api/units/{unit2Id}/rooms",
            new { name = "Unit2 Room" });

        var response = await _client.GetAsync($"/api/units/{unit1Id}/rooms");
        var rooms = await response.Content.ReadFromJsonAsync<List<RoomResponse>>();

        Assert.NotNull(rooms);
        Assert.Contains(rooms, r => r.Name == "Unit1 Room");
        Assert.DoesNotContain(rooms, r => r.Name == "Unit2 Room");
    }

    [Fact]
    public async Task GetRooms_NonMember_ReturnsForbidden()
    {
        // User 1 creates a unit and a room
        var token1 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token1);

        var unitId = await CreateUnitAsync();

        await _client.PostAsJsonAsync($"/api/units/{unitId}/rooms",
            new { name = "Secret Room" });

        // User 2 tries to access User 1's rooms
        var token2 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token2);

        var response = await _client.GetAsync($"/api/units/{unitId}/rooms");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    private record TokenResponse(string Token);
    private record UnitResponse(int Id, string Name, string Address, string Status);
    private record RoomResponse(int Id, int UnitId, string Name, int? Capacity, string Notes, bool IsActive);
}
