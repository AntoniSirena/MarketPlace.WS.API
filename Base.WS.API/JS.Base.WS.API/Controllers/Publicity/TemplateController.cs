using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.Models.Publicity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Publicity
{
    [Authorize]
    [RoutePrefix("api/template")]
    public class TemplateController : GenericApiController<Template>
    {

    }
}
