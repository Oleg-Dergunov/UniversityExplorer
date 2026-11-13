using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public interface ICountryService
{
    Task<List<Country>> GetAllCountriesAsync();
}