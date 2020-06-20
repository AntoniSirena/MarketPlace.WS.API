using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Models.Permission;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Permission
{
    [Authorize]
    [RoutePrefix("api/role")]
    public class RoleController : GenericApiController<Role>
    {

        private MyDBcontext db;
        private Response response;


        public RoleController()
        {
            db = new MyDBcontext();
            response = new Response();
        }


        public override IHttpActionResult Create(dynamic entity)
        {
            Guid guid = Guid.NewGuid();

            entity["Code"] = guid.ToString();

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }
    }
}
