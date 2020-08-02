using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Publicity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Publicity
{
    [Authorize]
    [RoutePrefix("api/portada")]
    public class PortadaController : ApiController
    {

        private MyDBcontext db;
        private Response response;


        public PortadaController()
        {
            db = new MyDBcontext();
            response = new Response();
        }


        [HttpGet]
        [Route("GetTemplate")]
        public IHttpActionResult GetTemplate(string operation)
        {
            var result = new PortadaDto();
            var template = db.Templates.Where(x => x.Operation == operation && x.IsActive == true && x.Enabled == true).FirstOrDefault();

            if (template != null)
            {
                result.Body = template.Body == null ? "Información en proceso, para ser publicada" : template.Body;
                response.Data = result;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code318;
                response.Message = InternalResponseCodeError.Message318;

                return Ok(response);
            }

            return Ok(response);
        }


    }
}
