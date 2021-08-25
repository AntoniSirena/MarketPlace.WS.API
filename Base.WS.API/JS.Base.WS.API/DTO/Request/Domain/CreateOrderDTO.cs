using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Request.Domain
{
    public class CreateOrderDTO
    {
        public long ArticleId { get; set; }
        public decimal Quantity { get; set; }
        public string ItemNote { get; set; }
    }
}