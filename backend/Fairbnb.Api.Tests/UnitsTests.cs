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

    private record TokenResponse(string Token);
    private record UnitResponse(int Id, string Name, string Address, string Status);
}
