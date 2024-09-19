using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Pets.API;
using Pets.Application;
using Pets.Contracts.Models;
using Pets.Contracts.Requests;
using Pets.Contracts.Responses;
using System.Text.Json;

namespace Pets.IntegrationTests;

public class PetControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public PetControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Returns_Successfull_PagedResponses_InDescendingOrder_BasedOn_CreatedDate()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("/api/pets?pageNumber=1&pageSize=2");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var results = JsonSerializer.Deserialize<List<PetResponse>>(responseString, options);

        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.True(results.OrderByDescending(r => r.CreatedDate).SequenceEqual(results));  
    }
}