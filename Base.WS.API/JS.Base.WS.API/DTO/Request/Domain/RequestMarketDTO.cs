using JS.Base.WS.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Request.Domain
{
    public class RequestMarketDTO
    {
        public Market Market { get; set; }
        public List<UploadFile> ImgDetails { get; set; }
    }
}