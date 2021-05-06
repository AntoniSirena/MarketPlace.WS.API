using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain.FreeMarket
{
    public class MarketDTO : Audit
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public int CurrencyId { get; set; }
        public  string Currency { get; set; }

        public int MarketTypeId { get; set; }
        public string MarketType { get; set; }

        public int ConditionId { get; set; }
        public string Condition { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }

        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }

        public string Ubication { get; set; }
        public string Description { get; set; }
        public long? PhoneNumber { get; set; }

        public string Img { get; set; }
        public string ImgPath { get; set; }
        public string ContenTypeShort { get; set; }
        public string ContenTypeLong { get; set; }

        public string CreationDate { get; set; }
    }
}