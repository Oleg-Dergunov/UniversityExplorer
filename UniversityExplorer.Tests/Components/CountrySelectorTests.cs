namespace UniversityExplorer.Tests.Components;

public class CountrySelectorTests : BunitContext
{
    private readonly List<Country> _countries = new()
    {
        new() { Name = "Ireland" },
        new() { Name = "Ukraine" }
    };

    [Fact]
    public void SelectsPlaceholder_WhenNoCountryIsSelected()
    {
        var cut = Render<CountrySelector>(parameters => parameters
            .Add(p => p.Countries, _countries));

        var firstOption = cut.Find("option");

        Assert.Contains("Choose a country", firstOption.TextContent);
        Assert.True(firstOption.HasAttribute("selected"));
    }

    [Fact]
    public void SelectingCountry_InvokesCallbackWithMatchingCountry()
    {
        Country? selected = null;

        var cut = Render<CountrySelector>(parameters => parameters
            .Add(p => p.Countries, _countries)
            .Add(p => p.OnCountrySelected, (Country country) => selected = country));

        cut.Find("select").Change("Ukraine");

        Assert.Same(_countries[1], selected);
    }

    [Fact]
    public void SelectingUnknownValue_DoesNotInvokeCallback()
    {
        Country? selected = null;

        var cut = Render<CountrySelector>(parameters => parameters
            .Add(p => p.Countries, _countries)
            .Add(p => p.OnCountrySelected, (Country country) => selected = country));

        cut.Find("select").Change("Atlantis");

        Assert.Null(selected);
    }
}