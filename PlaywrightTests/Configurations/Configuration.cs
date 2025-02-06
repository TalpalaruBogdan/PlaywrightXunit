namespace PlaywrightTests.Configurations;

public class BrowserConfig
{
     public string Browser { get; set; }
     public string Channel { get; set; }
     public int SlowMo { get; set; }
     public bool DevTools { get; set; }
     public bool Headless { get; set; }
}

public class TestData
{
     public string BaseUrl { get; set; }
}

// ConfigurationBase config = JsonConvert.DeserializeObject<ConfigurationBase>(myJsonResponse);
public class ConfigurationBase
{
     public BrowserConfig BrowserConfig { get; set; }

     public TestData TestData { get; set; }
}

