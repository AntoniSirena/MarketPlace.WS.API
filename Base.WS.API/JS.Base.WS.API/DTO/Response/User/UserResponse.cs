using JS.Base.WS.API.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.User
{
    public class UserResponse
    {
        public Configuration configuration { get; set; }
        public Profile profile { get; set; }
        public object permissions { get; set; }
    }
}