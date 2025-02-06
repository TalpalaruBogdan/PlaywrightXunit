using Microsoft.Playwright;
using PlaywrightTests.Configurations;

namespace PlaywrightTests.Fixtures;

public class PlaywrightFactory : IAsyncLifetime
{
     
     private IPlaywright? _playwright;
     private IBrowser? _browser;
     
     public IPage Page { get; private set; }

     public async Task InitializeAsync()
     {
          _playwright = await Playwright.CreateAsync();
          var options = new BrowserTypeLaunchOptions()
          {
               Headless = ConfigurationProvider.ConfigurationBase.BrowserConfig.Headless,
               SlowMo = ConfigurationProvider.ConfigurationBase.BrowserConfig.SlowMo,
               Devtools = ConfigurationProvider.ConfigurationBase.BrowserConfig.DevTools,
               Channel = ConfigurationProvider.ConfigurationBase.BrowserConfig.Channel
          };

          _browser = ConfigurationProvider.ConfigurationBase.BrowserConfig.Browser switch
          {
               "chrome" => await _playwright.Chromium.LaunchAsync(options),
               "firefox" => await _playwright.Firefox.LaunchAsync(options),
               "edge" => await _playwright.Chromium.LaunchAsync(options),
               _ => await _playwright.Chromium.LaunchAsync(options),
          };
          
          Page = await _browser.NewPageAsync();  
     }

     async Task IAsyncLifetime.DisposeAsync()
     {
          if (_playwright is IAsyncDisposable playwrightAsyncDisposable)
               await playwrightAsyncDisposable.DisposeAsync();
          else
               _playwright?.Dispose();
          await _browser!.DisposeAsync();
     }
}