using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.User
{
    public class UserDetails
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Image { get; set; }
        public Person Person { get; set; }
    }
}