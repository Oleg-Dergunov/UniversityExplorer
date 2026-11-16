namespace UniversityExplorer.Tests.Components;

public class PaginationTests : BunitContext
{
    [Fact]
    public void RendersNothing_WhenThereIsOnlyOnePage()
    {
        var cut = RenderPagination(currentPage: 1, totalPages: 1);

        Assert.Empty(cut.FindAll("nav"));
    }

    [Fact]
    public void ShowsAllPagesWithoutEllipsis_WhenThereAreFewPages()
    {
        var cut = RenderPagination(currentPage: 1, totalPages: 3);

        Assert.Equal(new[] { "«", "1", "2", "3", "»" }, GetItemLabels(cut));
    }

    [Fact]
    public void ShowsFirstLastAndNeighbouringPagesWithEllipsis_WhenThereAreManyPages()
    {
        var cut = RenderPagination(currentPage: 5, totalPages: 118);

        Assert.Equal(new[] { "«", "1", "…", "3", "4", "5", "6", "7", "…", "118", "»" }, GetItemLabels(cut));
    }

    [Fact]
    public void DisablesPreviousOnFirstPage_AndNextOnLastPage()
    {
        var first = RenderPagination(currentPage: 1, totalPages: 5);
        Assert.True(first.Find("button[aria-label='Previous page']").HasAttribute("disabled"));
        Assert.False(first.Find("button[aria-label='Next page']").HasAttribute("disabled"));

        var last = RenderPagination(currentPage: 5, totalPages: 5);
        Assert.True(last.Find("button[aria-label='Next page']").HasAttribute("disabled"));
    }

    [Fact]
    public void ClickingPageNumber_InvokesOnPageChangedWithThatPage()
    {
        int? selectedPage = null;

        var cut = Render<Pagination>(parameters => parameters
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.TotalPages, 5)
            .Add(p => p.OnPageChanged, (int pageNumber) => selectedPage = pageNumber));

        cut.FindAll("button").Single(b => b.TextContent.Trim() == "3").Click();

        Assert.Equal(3, selectedPage);
    }

    private IRenderedComponent<Pagination> RenderPagination(int currentPage, int totalPages) =>
        Render<Pagination>(parameters => parameters
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.TotalPages, totalPages));

    private static IEnumerable<string> GetItemLabels(IRenderedComponent<Pagination> cut) =>
        cut.FindAll("li").Select(li => li.TextContent.Trim());
}