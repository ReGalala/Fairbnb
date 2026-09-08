using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace Fairbnb.Api.Tests;

public class UnitsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UnitsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<string> GetTokenAsync()
    {
        var email = $"units-{Guid.NewGuid()}@test.com";
        var response = await _client.PostAsJsonAsync("/api/auth/register",
            new { email, password = "Test123!" });
        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return body!.Token;
    }

    [Fact]
    public async Task CreateUnit_ReturnsCreated()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/units",
            new { name = "Beach House", address = "123 Beach St" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var unit = await response.Content.ReadFromJsonAsync<UnitResponse>();
        Assert.NotNull(unit);
        Assert.Equal("Beach House", unit.Name);
    }

    [Fact]
    public async Task GetUnits_ReturnsUnits()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsJsonAsync("/api/units",
            new { name = "Mountain Cabin", address = "456 Hill Rd" });

        var response = await _client.GetAsync("/api/units");
        response.EnsureSuccessStatusCode();

        var units = await response.Content.ReadFromJsonAsync<List<UnitResponse>>();
        Assert.NotNull(units);
        Assert.True(units.Count > 0);
    }

    [Fact]
    public async Task GetUnits_WithoutToken_Returns401()
    {
        _client.DefaultRequestHeaders.Authorization = null;

        var response = await _client.GetAsync("/api/units");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateUnit_CreatesAdminMembership()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/units",
            new { name = "Admin Test Unit", address = "789 Admin St" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var units = await _client.GetAsync("/api/units");
        var list = await units.Content.ReadFromJsonAsync<List<UnitResponse>>();
        Assert.Contains(list!, u => u.Name == "Admin Test Unit");
    }

    [Fact]
    public async Task GetUnits_ReturnsOnlyOwnUnits()
    {
        // User 1 creates a unit
        var token1 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token1);

        await _client.PostAsJsonAsync("/api/units",
            new { name = "User1 Unit", address = "111 First St" });

        // User 2 creates a unit
        var token2 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token2);

        await _client.PostAsJsonAsync("/api/units",
            new { name = "User2 Unit", address = "222 Second St" });

        // User 2 should only see their own unit
        var response = await _client.GetAsync("/api/units");
        var units = await response.Content.ReadFromJsonAsync<List<UnitResponse>>();

        Assert.NotNull(units);
        Assert.Contains(units, u => u.Name == "User2 Unit");
        Assert.DoesNotContain(units, u => u.Name == "User1 Unit");
    }

    [Fact]
    public async Task UpdateUnit_AdminCanEdit()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var createResponse = await _client.PostAsJsonAsync("/api/units",
            new { name = "Old Name", address = "Old Address" });
        var created = await createResponse.Content.ReadFromJsonAsync<UnitResponse>();

        var updateResponse = await _client.PutAsJsonAsync($"/api/units/{created!.Id}",
            new { name = "New Name", address = "New Address" });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var updated = await updateResponse.Content.ReadFromJsonAsync<UnitResponse>();
        Assert.NotNull(updated);
        Assert.Equal("New Name", updated.Name);
        Assert.Equal("New Address", updated.Address);
        Assert.Equal(created.Id, updated.Id);
    }

    [Fact]
    public async Task UpdateUnit_NonMember_ReturnsForbidden()
    {
        // User 1 creates a unit
        var token1 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token1);

        var createResponse = await _client.PostAsJsonAsync("/api/units",
            new { name = "Private Unit", address = "Secret St" });
        var created = await createResponse.Content.ReadFromJsonAsync<UnitResponse>();

        // User 2 tries to update it
        var token2 = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token2);

        var updateResponse = await _client.PutAsJsonAsync($"/api/units/{created!.Id}",
            new { name = "Hacked Name", address = "Hacked Address" });

        Assert.Equal(HttpStatusCode.Forbidden, updateResponse.StatusCode);
    }

    [Fact]
    public async Task UpdateUnit_DoesNotCreateNewUnit()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        await _client.PostAsJsonAsync("/api/units",
            new { name = "Only Unit", address = "123 St" });

        var unitsBefore = await _client.GetAsync("/api/units");
        var listBefore = await unitsBefore.Content.ReadFromJsonAsync<List<UnitResponse>>();
        var countBefore = listBefore!.Count;

        var unit = listBefore.First();
        await _client.PutAsJsonAsync($"/api/units/{unit.Id}",
            new { name = "Updated Name", address = "Updated Address" });

        var unitsAfter = await _client.GetAsync("/api/units");
        var listAfter = await unitsAfter.Content.ReadFromJsonAsync<List<UnitResponse>>();

        Assert.Equal(countBefore, listAfter!.Count);
    }

    private record TokenResponse(string Token);
    private record UnitResponse(int Id, string Name, string Address, string Status);
}
