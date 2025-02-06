using Microsoft.Playwright;
using PlaywrightTests.Configurations;

namespace PlaywrightTests.PageObjects;

public class HomePage
{
     private IPage _page;

     public HomePage(IPage page)
     {
          this._page = page;
     }

     public async Task NavigateToAsync()
     {
          await _page.RunAndWaitForResponseAsync(async () =>
               {
                    await _page.GotoAsync(ConfigurationProvider.ConfigurationBase.TestData.BaseUrl);
               }, x => x.Status.Equals(200) 
                       && x.Url.Contains($"{ConfigurationProvider.ConfigurationBase.TestData.BaseUrl}/products?between=price,1,100&page=1")
               );
     }
}