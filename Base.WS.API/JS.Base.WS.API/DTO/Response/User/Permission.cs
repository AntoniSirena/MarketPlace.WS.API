using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.User
{
    public class Permission
    {
        public List<Entity> Entities { get; set; }
    }

    public class Entity
    {
        public string Description { get; set; }
        public string ShortName { get; set; }
        public List<EntityActions> EntityActions { get; set; }
    }

    public class EntityActions
    {
        public string ActionName { get; set; }
        public bool HasPermissio { get; set; }
    }
}