namespace UniversityExplorer.Tests.Services;

public class FavouritesServiceTests
{
    private readonly FakeLocalStorageService _storage = new();

    [Fact]
    public async Task ToggleAsync_AddsAndThenRemovesFavourite()
    {
        var service = new FavouritesService(_storage);
        var trinity = CreateUniversity("Trinity College", "Ireland");

        await service.ToggleAsync(trinity);
        Assert.True(service.IsFavourite(trinity));

        await service.ToggleAsync(trinity);
        Assert.False(service.IsFavourite(trinity));
    }

    [Fact]
    public async Task IsFavourite_TreatsSameNameInDifferentCountryAsDifferentUniversity()
    {
        var service = new FavouritesService(_storage);

        await service.ToggleAsync(CreateUniversity("University of London", "United Kingdom"));

        Assert.False(service.IsFavourite(CreateUniversity("University of London", "Canada")));
    }

    [Fact]
    public async Task IsFavourite_IgnoresCase()
    {
        var service = new FavouritesService(_storage);

        await service.ToggleAsync(CreateUniversity("Trinity College", "Ireland"));

        Assert.True(service.IsFavourite(CreateUniversity("trinity college", "ireland")));
    }

    [Fact]
    public async Task Favourites_ArePersisted_AndLoadedByNewServiceInstance()
    {
        var firstVisit = new FavouritesService(_storage);
        await firstVisit.ToggleAsync(CreateUniversity("Trinity College", "Ireland"));

        var secondVisit = new FavouritesService(_storage);
        await secondVisit.EnsureLoadedAsync();

        Assert.True(secondVisit.IsFavourite(CreateUniversity("Trinity College", "Ireland")));
    }

    private static University CreateUniversity(string name, string country) =>
        new() { Name = name, Country = country };
}