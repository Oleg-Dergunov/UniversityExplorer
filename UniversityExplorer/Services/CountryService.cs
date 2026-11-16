using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public class CountryService : ICountryService
{
    private readonly HttpClient _httpClient;

    public CountryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Country>> GetAllCountriesAsync()
    {
        var countries = await _httpClient.GetFromJsonAsync<List<Country>>("countries");

        return countries?
            .OrderBy(c => c.Name)
            .ToList() ?? new List<Country>();
    }
}