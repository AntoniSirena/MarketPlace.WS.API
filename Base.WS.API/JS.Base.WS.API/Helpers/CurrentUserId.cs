using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Helpers
{
    public static class CurrentUserId
    {
        private static long GetCurrentUserId()
        {
            var value = CacheStorage.GetValue("currentUserId");
            long response = Convert.ToInt64(value);

            return response;
        }
    }
}