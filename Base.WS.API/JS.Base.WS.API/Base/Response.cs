using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JS.Base.WS.API.Base
{
    public class Response : ResponseData<object>
    {
        public Response()
        {
            Code = "000";
            Message = string.Empty;
            MessageDetail = new object();
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public object MessageDetail { get; set; }

        public IHttpActionResult HttPCode { get; set; }
    }
    
}