using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Helpers
{
    public static class CurrentUser
    {
        public static long GetId()
        {
            var value = CacheStorage.GetValue("currentUserId");
            long response = Convert.ToInt64(value);

            return response;
        }

        public static string GetUserName()
        {
            var response = CacheStorage.GetValue("currentUserName");

            return response.ToString();
        }

        public static void DeleteId()
        {
            CacheStorage.Delete("currentUserId");
        }

        private static void DeleteUserName()
        {
            CacheStorage.Delete("currentUserName");
        }
    }
}