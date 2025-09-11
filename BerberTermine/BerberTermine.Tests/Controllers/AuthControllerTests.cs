using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOk()
    {
        var request = new { Username = "Adhurim", Password = "admin2025" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var request = new { Username = "Wrong", Password = "bad" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", request);

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }
}