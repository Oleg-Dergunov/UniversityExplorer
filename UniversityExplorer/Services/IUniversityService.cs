using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public interface IUniversityService
{
    Task<List<University>> GetUniversitiesByCountryAsync(string countryName);
    Task<List<University>> GetAllUniversitiesAsync();
}