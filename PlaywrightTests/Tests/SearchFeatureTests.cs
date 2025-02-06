using Microsoft.Playwright;
using PlaywrightTests.Configurations;
using PlaywrightTests.Fixtures;
using PlaywrightTests.PageObjects;

namespace PlaywrightTests;

public class SearchFeatureTests : IClassFixture<PlaywrightFactory>
{
     private readonly PlaywrightFactory _playwrightFactory;
     private readonly SearchFieldComponent _searchFieldComponent;
     private readonly SearchResultsComponent _mainSearchResultsComponent;
     private readonly SortComponent _sortComponent;
     private readonly CardFeedComponent _feedComponent;
     private IPage _page;

     public SearchFeatureTests(PlaywrightFactory playwrightFactory)
     {
          this._playwrightFactory = 
               playwrightFactory;
          _page = _playwrightFactory.Page;
          _searchFieldComponent = new SearchFieldComponent(_page);
          _mainSearchResultsComponent = new SearchResultsComponent(_page);
          _sortComponent = new SortComponent(_page);
          _feedComponent = new CardFeedComponent(_page);
     }


     [Fact]
     public async Task ShouldSortFeedItems()
     {
          await _page.GotoAsync(ConfigurationProvider.ConfigurationBase.TestData.BaseUrl);

          await _sortComponent.SetSortOrder("Name (A - Z)");

          var firstCardText = (await _feedComponent.GetProductNames()).FirstOrDefault();
          
          Assert.Equal("Adjustable Wrench", firstCardText.Trim());
     }
     
     [Theory(DisplayName = "Should display results for valid search")]
     [MemberData(nameof(searchTerms))]
     //[ClassData(nameof(SearchDataFixture)]
     public async Task ShouldDisplayResultsForValidSearch(string searchTerm)
     {
          await _page.GotoAsync(ConfigurationProvider.ConfigurationBase.TestData.BaseUrl);

          await _searchFieldComponent.SearchForTermAsync(searchTerm);
          
          var searchResultMessage = await _mainSearchResultsComponent.GetSearchTextCaption();

          // await _page.ScreenshotAsync(new PageScreenshotOptions()
          // {
          //      Path = "../../../Screenshots/search-results.png"
          // });
          
          Assert.Equal($"Searched for: {searchTerm}", searchResultMessage);
     }

     public static IEnumerable<object[]> searchTerms()
     {
          yield return ["pliers"];
          yield return ["cutters"];
     }
     
}