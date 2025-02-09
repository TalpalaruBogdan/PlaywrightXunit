using Microsoft.Playwright;
using PlaywrightTests.Configurations;

namespace PlaywrightTests.Fixtures;

public class PlaywrightFactory : IAsyncLifetime
{
     
     public IPlaywright? PlayWright;

     public IBrowserContext BrowserContext { get; private set; }
     
     public IBrowser Browser { get; private set; }

     public IPage Page { get; private set; }

     public async Task InitializeAsync()
     {
          this.PlayWright = await Playwright.CreateAsync();
          var options = new BrowserTypeLaunchOptions()
          {
               Headless = ConfigurationProvider.ConfigurationBase.BrowserConfig.Headless,
               SlowMo = ConfigurationProvider.ConfigurationBase.BrowserConfig.SlowMo,
               Devtools = ConfigurationProvider.ConfigurationBase.BrowserConfig.DevTools,
               Channel = ConfigurationProvider.ConfigurationBase.BrowserConfig.Channel
          };

          Browser = ConfigurationProvider.ConfigurationBase.BrowserConfig.Browser switch
          {
               "chrome" => await PlayWright.Chromium.LaunchAsync(options),
               "firefox" => await PlayWright.Firefox.LaunchAsync(options),
               "edge" => await PlayWright.Chromium.LaunchAsync(options),
               _ => await PlayWright.Chromium.LaunchAsync(options),
          };
          
          Page = await Browser.NewPageAsync();  
     }

     async Task IAsyncLifetime.DisposeAsync()
     {
          if (PlayWright is IAsyncDisposable playwrightAsyncDisposable)
               await playwrightAsyncDisposable.DisposeAsync();
          else
               PlayWright?.Dispose();
          await Browser!.DisposeAsync();
     }
}