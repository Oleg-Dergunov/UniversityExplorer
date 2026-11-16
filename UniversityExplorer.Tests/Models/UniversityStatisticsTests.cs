namespace UniversityExplorer.Tests.Models;

public class UniversityStatisticsTests
{
    [Fact]
    public void FromUniversities_CountsDuplicateUniversitiesOnce()
    {
        var universities = new List<University>
        {
            new() { Name = "Trinity College", Country = "Ireland" },
            new() { Name = "Trinity College", Country = "Ireland" },
            new() { Name = "Kyiv University", Country = "Ukraine" }
        };

        var statistics = UniversityStatistics.FromUniversities(universities);

        Assert.Equal(2, statistics.TotalUniversities);
    }

    [Fact]
    public void FromUniversities_GroupsCountriesCaseInsensitively_AndFindsTopCountry()
    {
        var universities = new List<University>
        {
            new() { Name = "Trinity College", Country = "Ireland" },
            new() { Name = "Dublin City University", Country = "ireland" },
            new() { Name = "Kyiv University", Country = "Ukraine" }
        };

        var statistics = UniversityStatistics.FromUniversities(universities);

        Assert.Equal(2, statistics.CountryCount);
        Assert.Equal("Ireland", statistics.TopCountry, ignoreCase: true);
        Assert.Equal(2, statistics.TopCountryUniversityCount);
    }

    [Fact]
    public void FromUniversities_WithEmptyList_ReturnsZeroValues()
    {
        var statistics = UniversityStatistics.FromUniversities(new List<University>());

        Assert.Equal(0, statistics.TotalUniversities);
        Assert.Equal(0, statistics.CountryCount);
        Assert.Null(statistics.TopCountry);
        Assert.Equal(0, statistics.TopCountryUniversityCount);
    }
}