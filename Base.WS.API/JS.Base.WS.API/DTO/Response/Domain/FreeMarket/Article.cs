using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain.FreeMarket
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public string Condition { get; set; }
        public string Ubication { get; set; }
        public string PhoneNumber { get; set; }
        public string CreationDate { get; set; }
    }
}