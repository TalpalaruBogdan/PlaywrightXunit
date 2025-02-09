using Microsoft.Playwright;
using PlaywrightTests.Fixtures;
using Xunit.Abstractions;

namespace PlaywrightTests.Tests.Api;

public class JsonPlaceholderApiTests: TestBase, IClassFixture<PlaywrightFactory>
{

     public JsonPlaceholderApiTests(PlaywrightFactory playwrightFactory, ITestOutputHelper output)
     {
          this._playwrightFactory = 
               playwrightFactory;
          this._playwright = _playwrightFactory.PlayWright!;
          _output = output;
     }

     [Fact]
     public async Task ShouldGetPosts()
     {
          // create request from 
          var request = await this._playwright.APIRequest.NewContextAsync(new() {
               BaseURL = "https://jsonplaceholder.typicode.com/",
          });

          var response = await request.GetAsync("todos/");
          
          Assert.True(response.Status == 200);
     }
     
     [Fact]
     public async Task ShouldCreatePosts()
     {
          var data = new Dictionary<string, string>
          {
               { "title", "[Bug] report 1" },
               { "body", "Bug description" },
               { "userId", "1"}
          };
          
          // create request from 
          var request = await this._playwright.APIRequest.NewContextAsync(new() {
               BaseURL = "https://jsonplaceholder.typicode.com/",
          });

          var response = await request.PostAsync("todos/", new APIRequestContextOptions()
          {
               DataObject = data
          });
          
          Assert.True(response.Status == 201);
     }
     
}