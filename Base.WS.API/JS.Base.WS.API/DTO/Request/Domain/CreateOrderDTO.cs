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


    public class CheckoutDTO
    {
        public long OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; }
    }

}