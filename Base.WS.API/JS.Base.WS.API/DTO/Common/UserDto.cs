using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Common
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}