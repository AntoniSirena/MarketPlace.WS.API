using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Authorization
{

    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : GenericApiController<User>
    {

    }
}
