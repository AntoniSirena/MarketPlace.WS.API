using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Request
{
    public class UserRequestExternal
    {
        public User user { get; set; }
        public string imagenBase64 { get; set; }
    }
}