using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AppointmentsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AppointmentsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAppointment_ShouldReturnOk()
    {
        var appointment = new { Name = "Aferdita", Phone = "123456789", Time = DateTime.Today.AddDays(1) };

        var response = await _client.PostAsJsonAsync("/api/appointments", appointment);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetAppointments_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/appointments");

        Assert.True(response.IsSuccessStatusCode);
    }
}