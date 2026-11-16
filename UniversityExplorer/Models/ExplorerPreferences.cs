namespace UniversityExplorer.Models;

public class ExplorerPreferences
{
    public string? CountryName { get; set; }
    public bool SortAscending { get; set; } = true;
    public bool ShowFavouritesOnly { get; set; }
}