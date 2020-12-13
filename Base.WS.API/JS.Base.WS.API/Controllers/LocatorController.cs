using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.PersonProfile;
using JS.Base.WS.API.Services;
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
    [RoutePrefix("api/locator")]
    [Authorize]
    public class LocatorController : GenericApiController<Locator>
    {
        private MyDBcontext db;
        private Response response;
        private LocatorService LocatorService;


        public LocatorController()
        {
            db = new MyDBcontext();
            response = new Response();
            LocatorService = new LocatorService();
        }

        private long currentUserId = CurrentUser.GetId();

        public override IHttpActionResult Create(dynamic entity)
        {
            var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();
            bool updateLocatorIsMainFalse = false;
            string isMain = entity["IsMain"];
            string inputDescription = entity["Description"];

            if (currentUser.PersonId != null)
            {
                entity["PersonId"] = currentUser.PersonId.ToString();
            }
            else
            {
                response.Code = InternalResponseCodeError.Code305;
                response.Message = InternalResponseCodeError.Message305;
                return Ok(response);
            }

            bool recordExists = db.Locators.Where(x => x.PersonId == currentUser.PersonId).ToList().Exists(x => x.Description == inputDescription);
            if (recordExists)
            {
                response.Code = InternalResponseCodeError.Code302;
                response.Message = InternalResponseCodeError.Message302;
                return Ok(response);
            }

            if (!string.IsNullOrEmpty(isMain))
            {
                if (isMain.Equals("True"))
                {
                    updateLocatorIsMainFalse = LocatorService.updateLocatorIsMainFalse();
                }
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            return base.Create(input);
        }

        public override IHttpActionResult Update(dynamic entity)
        {
            string isMain = entity["IsMain"];
            bool updateLocatorIsMainFalse = false;

            if (isMain.Equals("True"))
            {
                updateLocatorIsMainFalse = LocatorService.updateLocatorIsMainFalse();
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            return base.Update(input);
        }
    }
}
