using Microsoft.Playwright;

namespace PlaywrightTests.PageObjects;

public class SearchResultsComponent
{
     private IPage _page;
     private ILocator _searchTextCaption => _page.Locator("[data-test=search-caption]");
     
     public SearchResultsComponent(IPage page)
     {
          this._page = page;
     }
     
     public async Task<string> GetSearchTextCaption() => await _searchTextCaption.TextContentAsync();
}