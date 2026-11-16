namespace UniversityExplorer.Tests.Fakes;

public class FakeCountryService : ICountryService
{
    public List<Country> Countries { get; set; } = new();

    public Task<List<Country>> GetAllCountriesAsync() => Task.FromResult(Countries);
}