namespace UniversityExplorer.Tests.Components;

public class StatisticsOverviewTests : BunitContext
{
    private readonly FakeUniversityService _universityService = new();

    public StatisticsOverviewTests()
    {
        Services.AddSingleton<IUniversityService>(_universityService);
        Services.AddLogging();
    }

    [Fact]
    public void ShowsStatistics_WhenApiSucceeds()
    {
        _universityService.Universities = new()
        {
            new() { Name = "Trinity College", Country = "Ireland" },
            new() { Name = "Dublin City University", Country = "Ireland" },
            new() { Name = "Kyiv University", Country = "Ukraine" }
        };

        var cut = Render<StatisticsOverview>();

        cut.WaitForAssertion(() =>
        {
            Assert.Contains("Universities worldwide", cut.Markup);
            Assert.Contains("Ireland", cut.Markup);
            Assert.Contains("Most universities (2)", cut.Markup);
        });
    }

    [Fact]
    public void ShowsErrorAlert_WhenApiFails()
    {
        _universityService.ShouldFail = true;

        var cut = Render<StatisticsOverview>();

        cut.WaitForAssertion(() =>
            Assert.Contains("Unable to retrieve university statistics", cut.Markup));
    }
}