namespace UniversityExplorer.Tests.Fakes;

public class FakeUniversityService : IUniversityService
{
    public List<University> Universities { get; set; } = new();

    public bool ShouldFail { get; set; }

    public Task<List<University>> GetUniversitiesByCountryAsync(string countryName) =>
        ShouldFail
            ? Task.FromException<List<University>>(new HttpRequestException("API unavailable"))
            : Task.FromResult(Universities.Where(u => u.Country == countryName).ToList());

    public Task<List<University>> GetAllUniversitiesAsync() =>
        ShouldFail
            ? Task.FromException<List<University>>(new HttpRequestException("API unavailable"))
            : Task.FromResult(Universities);
}