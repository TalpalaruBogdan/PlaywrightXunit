namespace PlaywrightTests.Mocks;

public class QueryItemsMockProvider
{
     private static string _itemsMockResponse;

     public static string ItemsMockResponse
     {
          get
          {
               if (_itemsMockResponse is null)
               {
                    using (var reader = new StreamReader("Mocks/itemsQuery.json"))
                    {
                         _itemsMockResponse = reader.ReadToEnd();
                    }
               }
               return _itemsMockResponse;
          }
          set
          {
               _itemsMockResponse = value;
          }
     }
}