using Microsoft.Playwright;
using PlaywrightTests.Fixtures;
using PlaywrightTests.Mocks;
using PlaywrightTests.PageObjects;
using Polly;
using Xunit.Abstractions;

namespace PlaywrightTests;

public class SearchFeatureTests : UITestBase, IClassFixture<PlaywrightFactory>
{

     public SearchFeatureTests(PlaywrightFactory playwrightFactory, ITestOutputHelper output)
     {
          this._playwrightFactory = 
               playwrightFactory;
          _page = _playwrightFactory.Page;
          _searchFieldComponent = new SearchFieldComponent(_page);
          _mainSearchResultsComponent = new SearchResultsComponent(_page);
          _sortComponent = new SortComponent(_page);
          _feedComponent = new CardFeedComponent(_page);
          _homePage = new HomePage(_page);
          _output = output;
          _page.Request += (_, request) => _output.WriteLine(request.Method + " " + request.Url);
     }

     [Fact]
     public async Task ShouldSortFeedItems()
     {
          await _homePage.NavigateToAsync();

          await _page.RunAndWaitForResponseAsync(async () =>
          {
               await _sortComponent.SetSortOrder("Name (A - Z)");
          }, 
               response => response.Url.Contains("products?sort=name,asc&between=price,1,100&page=0"));
          
          await Polly.Policy
               .Handle<Exception>()
               .WaitAndRetryAsync(3, attempt  => TimeSpan.FromSeconds(3))
               .ExecuteAsync( async () =>
               {          
                    var firstCard = await _feedComponent.GetProductNames();
                    Assert.Equal("Adjustable Wrench", firstCard.First().Trim());
               });
     }
     
     [Theory(DisplayName = "Should display results for valid search")]
     [MemberData(nameof(searchTerms))]
     public async Task ShouldDisplayResultsForValidSearch(string searchTerm)
     {
          await _homePage.NavigateToAsync();
          
          await _searchFieldComponent.SearchForTermAsync(searchTerm);
          
          var searchResultMessage = await _mainSearchResultsComponent.GetSearchTextCaption();
          
          Assert.Equal($"Searched for: {searchTerm}", searchResultMessage);
     }

     [Fact]
     public async Task ShouldLoadPageWhenImageResourcesUnavailable()
     {
          _page.RouteAsync("**/*", async route =>
          {
               if (route.Request.Url.Contains("avif"))
               {
                    await route.AbortAsync();
               }
               else await route.ContinueAsync();
          });
          
          await _homePage.NavigateToAsync();

          int cardsDisplayed = await _mainSearchResultsComponent.GetCardsCount();
          Assert.True(cardsDisplayed > 0);
     }

     public static IEnumerable<object[]> searchTerms()
     {
          yield return ["pliers"];
          yield return ["cutters"];
     }
}