using Newtonsoft.Json;
using System.Collections.Generic;

namespace EcommerceSystem.Helper
{
   public static class SessionHelper
   {
      public static void SetObjInSession(this ISession session,string key,object value)
      {
         session.SetString(key,JsonConvert.SerializeObject(value));
      }
      public static List<T> GetObjInSession<T>(this ISession session,string key)
      {
         var obj = session.GetString(key);
         return obj == null ? default(List<T>) : JsonConvert.DeserializeObject<List<T>>(obj);
      }
   }
}
