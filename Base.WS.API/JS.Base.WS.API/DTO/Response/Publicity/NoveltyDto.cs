using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Publicity
{
    public class NoveltyDto: Audit
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public int NoveltyTypeId { get; set; }
        public string NoveltyType { get; set; }
        public string Img { get; set; }
        public string ImgPath { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsPublic { get; set; }

    }
}