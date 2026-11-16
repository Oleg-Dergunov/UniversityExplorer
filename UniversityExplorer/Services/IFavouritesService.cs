using UniversityExplorer.Models;

namespace UniversityExplorer.Services;

public interface IFavouritesService
{
    IReadOnlyList<University> Favourites { get; }
    Task EnsureLoadedAsync();
    bool IsFavourite(University university);
    Task ToggleAsync(University university);
}