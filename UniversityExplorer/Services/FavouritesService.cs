using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public class FavouritesService : IFavouritesService
{
    private const string StorageKey = "universityExplorer.favourites";

    private readonly ILocalStorageService _localStorage;
    private List<University> _favourites = new();
    private bool _isLoaded;

    public FavouritesService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public IReadOnlyList<University> Favourites => _favourites;

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
        {
            return;
        }

        _favourites = await _localStorage.GetAsync<List<University>>(StorageKey) ?? new();
        _isLoaded = true;
    }

    public bool IsFavourite(University university) =>
        _favourites.Any(f => IsSameUniversity(f, university));

    public async Task ToggleAsync(University university)
    {
        await EnsureLoadedAsync();

        var existing = _favourites.FirstOrDefault(f => IsSameUniversity(f, university));

        if (existing is null)
        {
            _favourites.Add(university);
        }
        else
        {
            _favourites.Remove(existing);
        }

        await _localStorage.SetAsync(StorageKey, _favourites);
    }

    // University names are not unique worldwide, so the country is part of the identity
    private static bool IsSameUniversity(University a, University b) =>
        string.Equals(a.Name, b.Name, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(a.Country, b.Country, StringComparison.OrdinalIgnoreCase);
}