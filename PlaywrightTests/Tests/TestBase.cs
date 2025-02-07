using Microsoft.Playwright;
using PlaywrightTests.Fixtures;
using PlaywrightTests.PageObjects;
using Xunit.Abstractions;

namespace PlaywrightTests;

public class TestBase
{
     protected PlaywrightFactory _playwrightFactory;
     protected SearchFieldComponent _searchFieldComponent;
     protected SearchResultsComponent _mainSearchResultsComponent;
     protected SortComponent _sortComponent;
     protected CardFeedComponent _feedComponent;
     protected HomePage _homePage;
     protected IPage _page;
     protected ITestOutputHelper _output;
}