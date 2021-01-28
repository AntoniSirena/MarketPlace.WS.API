using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Request
{
    public class UploadFile
    {
        public string base64 { get; set; }
        public string name { get; set; }
        public long size { get; set; }
        public string type { get; set; }
    }
}