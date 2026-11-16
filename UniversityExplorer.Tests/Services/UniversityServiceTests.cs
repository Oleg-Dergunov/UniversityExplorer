using Microsoft.Extensions.Caching.Memory;

namespace UniversityExplorer.Tests.Services;

public class UniversityServiceTests
{
    [Theory]
    [InlineData("United States of America", "?country=United%20States")]
    [InlineData("Turkey", "?country=Turkiye")]
    [InlineData("Ireland", "?country=Ireland")]
    public async Task GetUniversitiesByCountryAsync_UsesUniversitiesApiCountryName(string countryName, string expectedQuery)
    {
        var handler = new FakeHttpMessageHandler("[]");
        var service = CreateService(handler);

        await service.GetUniversitiesByCountryAsync(countryName);

        Assert.Equal(expectedQuery, handler.RequestedUris.Single().Query);
    }

    [Fact]
    public async Task GetUniversitiesByCountryAsync_RemovesDuplicates_AndSortsByName()
    {
        var handler = new FakeHttpMessageHandler("""
            [
              { "name": "Trinity College", "country": "Ireland" },
              { "name": "Dublin City University", "country": "Ireland" },
              { "name": "Trinity College", "country": "Ireland" }
            ]
            """);
        var service = CreateService(handler);

        var result = await service.GetUniversitiesByCountryAsync("Ireland");

        Assert.Equal(new[] { "Dublin City University", "Trinity College" }, result.Select(u => u.Name));
    }

    [Fact]
    public async Task GetAllUniversitiesAsync_CachesResult_AndCallsApiOnlyOnce()
    {
        var handler = new FakeHttpMessageHandler("[]");
        var service = CreateService(handler);

        await service.GetAllUniversitiesAsync();
        await service.GetAllUniversitiesAsync();

        Assert.Single(handler.RequestedUris);
    }

    private static UniversityService CreateService(FakeHttpMessageHandler handler) =>
        new(new HttpClient(handler) { BaseAddress = new Uri("http://test/") },
            new MemoryCache(new MemoryCacheOptions()));
}