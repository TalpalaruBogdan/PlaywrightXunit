using Microsoft.Playwright;

namespace PlaywrightTests.PageObjects;

public class SortComponent
{
     private IPage _page;
     
     public SortComponent(IPage page)
     {
          this._page = page;
     }    

     public async Task SetSortOrder(string option)
     {
          await _page.Locator("[data-test=sort]").SelectOptionAsync(option);
     }
     
}