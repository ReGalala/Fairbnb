using System.Net;
using System.Net.Http.Json;

namespace Fairbnb.Api.Tests;

public class AuthTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public AuthTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task Register_ReturnsToken()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register",
        new { email = "test@test.com", password = "Test123!"});

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body.Token));
    }
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        //First register
        await _client.PostAsJsonAsync("/api/auth/register",
        new { email = "login@test.com", password = "Test123!"});

        //Then login
        var response = await _client.PostAsJsonAsync("/api/auth/login",
        new { email = "login@test.com", password = "Test123!"});

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<TokenResponse>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body.Token));
    }
    [Fact]
    public async Task Login_WithInvalidCredentials_Returns401()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login",
        new { email = "nobody@test.com", password = "Wrong123!"});

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    private record TokenResponse(string Token);
}