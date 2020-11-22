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
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLoginTimeEnd { get; set; }
        public bool IsOnline { get; set; }
        public RoleDto Role { get; set; }
        public Person Person { get; set; }
   
    }

    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLoginTimeEnd { get; set; }
        public bool IsOnline { get; set; }
        public RoleDto Role { get; set; }
    }

    public class RoleDto
    {
        public string Description { get; set; }
        public string Parent { get; set; }
    }
}