using Microsoft.Playwright;

namespace PlaywrightTests.PageObjects;

public class SortComponent
{
     private IPage _page;

     private ILocator _orderSelect => _page.Locator("[data-test=sort]");
     
     public SortComponent(IPage page)
     {
          this._page = page;
     }    

     public async Task SetSortOrder(string option)
     {
          await _orderSelect.SelectOptionAsync(option);
     }
     
}