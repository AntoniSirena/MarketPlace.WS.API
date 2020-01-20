using JS.Base.WS.API.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.User
{
    public class UserResponse
    {
        public Configuration Configuration { get; set; }
        public Profile Profile { get; set; }
        public Permission Permissions { get; set; }
    }
}