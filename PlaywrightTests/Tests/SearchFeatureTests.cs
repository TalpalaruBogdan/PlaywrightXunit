using Microsoft.Playwright;
using PlaywrightTests.Configurations;
using PlaywrightTests.Fixtures;
using PlaywrightTests.PageObjects;
using Xunit.Abstractions;

namespace PlaywrightTests;

public class SearchFeatureTests : TestBase, IClassFixture<PlaywrightFactory>
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

          await _sortComponent.SetSortOrder("Name (A - Z)");

          var firstCardText = (await _feedComponent.GetProductNames()).FirstOrDefault();
          
          Assert.Equal("Adjustable Wrench", firstCardText.Trim());
     }
     
     [Theory(DisplayName = "Should display results for valid search")]
     [MemberData(nameof(searchTerms))]
     //[ClassData(nameof(SearchDataFixture)]
     public async Task ShouldDisplayResultsForValidSearch(string searchTerm)
     {
          await _homePage.NavigateToAsync();
          
          await _searchFieldComponent.SearchForTermAsync(searchTerm);
          
          var searchResultMessage = await _mainSearchResultsComponent.GetSearchTextCaption();

          // await _page.ScreenshotAsync(new PageScreenshotOptions()
          // {
          //      Path = "../../../Screenshots/search-results.png"
          // });
          
          Assert.Equal($"Searched for: {searchTerm}", searchResultMessage);
     }

     [Fact]
     public async Task ShouldDisplayAltTextWhenImagesAreNotLoaded()
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
          

     }

     public static IEnumerable<object[]> searchTerms()
     {
          yield return ["pliers"];
          yield return ["cutters"];
     }
     
}