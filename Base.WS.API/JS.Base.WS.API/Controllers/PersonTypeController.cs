using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models;
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
    [RoutePrefix("api/personType")]
    [Authorize]
    public class PersonTypeController : GenericApiController<PersonType>
    {

        private MyDBcontext db;
        private Response response;


        public PersonTypeController()
        {
            db = new MyDBcontext();
            response = new Response();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            string inputCode = entity["Code"];
            var personTypeCode = db.PersonTypes.Where(x => x.Code == inputCode).FirstOrDefault();

            if (personTypeCode != null)
            {
                response.Code = InternalResponseCodeError.Code303;
                response.Message = InternalResponseCodeError.Message303;

                return Ok(response);
            }

            string inputDescription = entity["Description"];
            var personTypeDescription = db.PersonTypes.Where(x => x.Description == inputDescription).FirstOrDefault();

            if (personTypeDescription != null)
            {
                response.Code = InternalResponseCodeError.Code304;
                response.Message = InternalResponseCodeError.Message304;

                return Ok(response);
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }


        public override IHttpActionResult Update(dynamic entity)
        {
            string inputCode = entity["Code"];
            var personTypeCode = db.PersonTypes.Where(x => x.Code == inputCode).FirstOrDefault();

            if (personTypeCode != null)
            {
                response.Code = InternalResponseCodeError.Code303;
                response.Message = InternalResponseCodeError.Message303;

                return Ok(response);
            }

            string inputDescription = entity["Description"];
            var personTypeDescription = db.PersonTypes.Where(x => x.Description == inputDescription).FirstOrDefault();

            if (personTypeDescription != null)
            {
                response.Code = InternalResponseCodeError.Code304;
                response.Message = InternalResponseCodeError.Message304;

                return Ok(response);
            }

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Update(input);
        }

    }
}
