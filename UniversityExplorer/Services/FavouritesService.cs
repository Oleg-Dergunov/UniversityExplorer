using System.Text.Json;
using Microsoft.JSInterop;
using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public class FavouritesService : IFavouritesService
{
    private const string StorageKey = "universityExplorer.favourites";

    private readonly IJSRuntime _js;
    private List<University> _favourites = new();
    private bool _isLoaded;

    public FavouritesService(IJSRuntime js)
    {
        _js = js;
    }

    public IReadOnlyList<University> Favourites => _favourites;

    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
        {
            return;
        }

        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);

            if (!string.IsNullOrWhiteSpace(json))
            {
                _favourites = JsonSerializer.Deserialize<List<University>>(json) ?? new();
            }
        }
        catch (JsonException)
        {
            // Corrupted data in storage: start with an empty list
            _favourites = new();
        }

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

        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, JsonSerializer.Serialize(_favourites));
    }

    // University names are not unique worldwide, so the country is part of the identity
    private static bool IsSameUniversity(University a, University b) =>
        string.Equals(a.Name, b.Name, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(a.Country, b.Country, StringComparison.OrdinalIgnoreCase);
}