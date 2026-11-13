using System.Net.Http.Json;
using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public class UniversityService : IUniversityService
{
    private readonly HttpClient _httpClient;

    // countries.dev and the Universities API use different names for some countries
    private static readonly Dictionary<string, string> CountryNameOverrides =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["United States of America"] = "United States",
            ["United Kingdom of Great Britain and Northern Ireland"] = "United Kingdom",
            ["Korea (Republic of)"] = "Korea, Republic of",
            ["Iran (Islamic Republic of)"] = "Iran",
            ["Bolivia (Plurinational State of)"] = "Bolivia, Plurinational State of",
            ["Venezuela (Bolivarian Republic of)"] = "Venezuela, Bolivarian Republic of",
            ["Moldova (Republic of)"] = "Moldova, Republic of",
            ["Congo (Democratic Republic of the)"] = "Congo, the Democratic Republic of the"
        };

    public UniversityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<University>> GetUniversitiesByCountryAsync(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName))
        {
            return new List<University>();
        }

        var apiCountryName = CountryNameOverrides.TryGetValue(countryName, out var mapped)
            ? mapped
            : countryName;

        var universities = await _httpClient.GetFromJsonAsync<List<University>>(
            $"search?country={Uri.EscapeDataString(apiCountryName)}");

        // The API occasionally returns duplicate entries
        return universities?
            .DistinctBy(u => u.Name)
            .OrderBy(u => u.Name)
            .ToList() ?? new List<University>();
    }
}