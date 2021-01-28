using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class MarketImgDetail : Audit
    {
        [Key]
        public long Id { get; set; }
        public long MarketId { get; set; }
        public string ImgPath { get; set; }
        public string ContenTypeShort { get; set; }
        public string ContenTypeLong { get; set; }

        [ForeignKey("MarketId")]
        public virtual Market Market { get; set; }
    }
}