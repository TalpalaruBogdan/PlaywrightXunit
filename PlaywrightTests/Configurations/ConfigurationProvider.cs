using System.Text.Json;

namespace PlaywrightTests.Configurations;

public class ConfigurationProvider
{
     private static ConfigurationBase? _configuration;

     public static ConfigurationBase ConfigurationBase
     {
          get
          {
               if (_configuration is null)
               {
                    _configuration = new ();
                    using (var reader = new StreamReader("appSettings.json"))
                    {
                         var json = reader.ReadToEnd();
                         _configuration = JsonSerializer.Deserialize<ConfigurationBase>(json);
                    }
               }
               return _configuration!;
          }
          set
          {
               _configuration = value;    
          }
     }
}