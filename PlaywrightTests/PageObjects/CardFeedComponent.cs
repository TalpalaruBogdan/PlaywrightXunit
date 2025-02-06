using Microsoft.Playwright;

namespace PlaywrightTests.PageObjects;

public class CardFeedComponent
{
     private IPage _page;

     private ILocator _cardElements => _page.Locator(".card");

     private ILocator _cardNames => _cardElements.Locator("[data-test=product-name]");
     
     public CardFeedComponent(IPage page)
     {
          this._page = page;
     }

     public Task<IReadOnlyList<string>> GetProductNames() => _cardNames.AllTextContentsAsync();
}