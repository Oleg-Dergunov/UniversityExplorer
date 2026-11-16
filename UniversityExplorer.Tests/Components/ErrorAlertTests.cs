namespace UniversityExplorer.Tests.Components;

public class ErrorAlertTests : BunitContext
{
    [Fact]
    public void ShowsMessage_AndHidesRetryButton_WhenNoRetryHandlerIsProvided()
    {
        var cut = Render<ErrorAlert>(parameters => parameters
            .Add(p => p.Message, "Something went wrong"));

        Assert.Contains("Something went wrong", cut.Markup);
        Assert.Empty(cut.FindAll("button"));
    }

    [Fact]
    public void ClickingTryAgain_InvokesRetryHandler()
    {
        var retried = false;

        var cut = Render<ErrorAlert>(parameters => parameters
            .Add(p => p.OnRetry, () => retried = true));

        cut.Find("button").Click();

        Assert.True(retried);
    }
}