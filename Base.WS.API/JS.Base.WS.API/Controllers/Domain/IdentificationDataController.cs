using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Domain
{
    [RoutePrefix("api/identificationData")]
    [Authorize]
    public class IdentificationDataController : GenericApiController<IdentificationData>
    {

        private MyDBcontext db;
        private Response response;
        private AccompanyingInstrumentService accompanyingInstrumentService;

        public IdentificationDataController()
        {
            db = new MyDBcontext();
            response = new Response();
            accompanyingInstrumentService = new AccompanyingInstrumentService();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            //Creating Accompanying Instrument Request
            var inProcess = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InProcess).FirstOrDefault();
            var request = new AccompanyingInstrumentRequest()
            {
                StatusId = inProcess.Id,
                DocentId = Convert.ToInt64(entity["DocentId"]),
                OpeningDate = DateTime.Now,
                ClosingDate = null,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestResult = db.AccompanyingInstrumentRequests.Add(request);
            db.SaveChanges();

            entity["RequestId"] = requestResult.Id;
            entity["VisitIdB"] = null;
            entity["VisitIdC"] = null;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }


        [HttpGet]
        [Route("GetAccompInstRequest")]
        public IHttpActionResult GetAccompInstRequest()
        {
            var result = accompanyingInstrumentService.GetAccompInstRequest();

            return Ok(result);
        }
    }
}
