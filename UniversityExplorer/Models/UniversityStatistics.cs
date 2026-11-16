namespace UniversityExplorer.Models;

public record UniversityStatistics(
    int TotalUniversities,
    int CountryCount,
    string? TopCountry,
    int TopCountryUniversityCount)
{
    public static UniversityStatistics FromUniversities(IReadOnlyCollection<University> universities)
    {
        // The API contains some duplicate entries, which are counted once
        var unique = universities
            .Where(u => !string.IsNullOrWhiteSpace(u.Name) && !string.IsNullOrWhiteSpace(u.Country))
            .DistinctBy(u => (u.Name.Trim().ToLowerInvariant(), u.Country.Trim().ToLowerInvariant()))
            .ToList();

        var countries = unique
            .GroupBy(u => u.Country.Trim(), StringComparer.OrdinalIgnoreCase)
            .Select(g => new { Country = g.Key, Count = g.Count() })
            .OrderByDescending(c => c.Count)
            .ToList();

        var top = countries.FirstOrDefault();

        return new UniversityStatistics(
            TotalUniversities: unique.Count,
            CountryCount: countries.Count,
            TopCountry: top?.Country,
            TopCountryUniversityCount: top?.Count ?? 0);
    }
}