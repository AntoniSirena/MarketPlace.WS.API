using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Publicity
{
    public class NoveltiesByTypeDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgBase64 { get; set; }
        public string ImgPath { get; set; }
        public string ContenTypeShort { get; set; }
        public string ContenTypeLong { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}