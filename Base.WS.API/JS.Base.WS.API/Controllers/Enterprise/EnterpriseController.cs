using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Enterprise;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers
{

    [Authorize]
    [RoutePrefix("api/enterprise")]
    public class EnterpriseController : ApiController
    {

        private MyDBcontext db;
        private Response response;

        private long currentUserId = CurrentUser.GetId();

        public EnterpriseController()
        {
            db = new MyDBcontext();
            response = new Response();
        }




        //public override IHttpActionResult Create(dynamic entity)
        //{
        //    entity["UserId"] = currentUserId.ToString();

        //    long enterpriseId = db.Enterprises.Where(x => x.UserId == currentUserId && x.IsActive == true).Select(y => y.Id).FirstOrDefault();

        //    //Validate enterprise
        //    if (enterpriseId > 0)
        //    {
        //        response.Code = InternalResponseCodeError.Code325;
        //        response.Message = InternalResponseCodeError.Message325;
        //        return Ok(response);
        //    }



        //    object input = JsonConvert.DeserializeObject<object>(entity.ToString());

        //    return base.Create(input);
        //}

    }
}
