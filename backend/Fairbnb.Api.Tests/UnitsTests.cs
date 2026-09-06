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

    private record TokenResponse(string Token);
    private record UnitResponse(int Id, string Name, string Address, string Status);
}
