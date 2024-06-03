using X.PagedList.Mvc.Core;

public static class PaginationHelper
{
    public static PagedListRenderOptions GetPaginationOptions()
    {
        return new PagedListRenderOptions
        {
            DisplayLinkToFirstPage = PagedListDisplayMode.Never,
            DisplayLinkToLastPage = PagedListDisplayMode.Never,
            DisplayLinkToPreviousPage = PagedListDisplayMode.Never,
            DisplayLinkToNextPage = PagedListDisplayMode.Never,
            DisplayLinkToIndividualPages = true,
            MaximumPageNumbersToDisplay = 5,
            EllipsesFormat = "&#8230;",
            LinkToFirstPageFormat = "First",
            LinkToPreviousPageFormat = "Previous",
            LinkToNextPageFormat = "Next",
            LinkToLastPageFormat = "Last",
            ContainerDivClasses = new[] { "pagination-container" },
            UlElementClasses = new[] { "pagination", "pagination-lg", "justify-content-center" },
            LiElementClasses = new[] { "page-item" },
            PageClasses = new[] { "page-link" },
            ClassToApplyToFirstListItemInPager = "page-item",
            ClassToApplyToLastListItemInPager = "page-item",
            NextElementClass = "page-item",
            ActiveLiElementClass = "page-item active",
            EllipsesElementClass = "page-item disabled",
            PreviousElementClass = "page-item"
        };
    }
}
