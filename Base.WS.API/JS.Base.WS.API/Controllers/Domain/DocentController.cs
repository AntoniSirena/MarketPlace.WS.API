using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Domain
{

    [RoutePrefix("api/docent")]
    [Authorize]
    public class DocentController : GenericApiController<Docent>
    {

        private MyDBcontext db;
        private Response response;
        private DocentService docentService;

        public DocentController()
        {
            db = new MyDBcontext();
            response = new Response();
            docentService = new DocentService();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            string documentNumber = entity["DocumentNumber"];
            //string expresion = @"[a-zA-Z' '*-_]";

            //documentNumber = Regex.Replace(documentNumber, expresion, "");

            var docent = db.Docents.Where(x => x.DocumentNumber.Equals(documentNumber) && x.IsActive == true).FirstOrDefault();

            if (docent != null)
            {
                response.Code = InternalResponseCodeError.Code311;
                response.Message = InternalResponseCodeError.Message311;

                return Ok(response);
            }

            entity.FullName = entity["FirstName"] + " " + entity["SecondName"] + " " + entity["Surname"] + " " + entity["SecondSurname"];

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }

        public override IHttpActionResult Update(dynamic entity)
        {

            string inputDocumentNumber = entity["DocumentNumber"];
            int idInput = Convert.ToInt32(entity["Id"]);
            var docent = db.Docents.Where(x => x.DocumentNumber.Equals(inputDocumentNumber) && x.IsActive == true).FirstOrDefault();

            if (docent != null)
            {
                if (idInput != docent.Id)
                {
                    response.Code = InternalResponseCodeError.Code311;
                    response.Message = InternalResponseCodeError.Message311;

                    return Ok(response);
                }
            }

            entity.FullName = entity["FirstName"] + " " + entity["SecondName"] + " " + entity["Surname"] + " " + entity["SecondSurname"];

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Update(input);
        }


        [HttpGet]
        [Route("GetDocentDetails/{docentId}")]
        public IHttpActionResult GetDocentDetails(long docentId)
        {
            var result = docentService.GetDocentDetails(docentId);

            return Ok(result);
        }

    }
}
