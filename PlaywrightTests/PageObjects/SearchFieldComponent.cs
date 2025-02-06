using Microsoft.Playwright;

namespace PlaywrightTests.PageObjects;

public class SearchFieldComponent
{
     private IPage _page;
     private string _searchInputFieldLocator = "#search-query";
     private string _searchSubmitLocator = "[data-test=search-submit]";
     
     public SearchFieldComponent(IPage page)
     {
          this._page = page;
     }

     public async Task SearchForTermAsync(string searchTerm)
     {
          await _page.RunAndWaitForResponseAsync(async () =>
          {
               await _page.Locator(_searchInputFieldLocator).FillAsync(searchTerm);
               await _page.Locator(_searchSubmitLocator).ClickAsync();
          }, response => response.Url.Contains(searchTerm)
                         && response.Status == 200
                         && response.Request.Method == "GET");
     }
}