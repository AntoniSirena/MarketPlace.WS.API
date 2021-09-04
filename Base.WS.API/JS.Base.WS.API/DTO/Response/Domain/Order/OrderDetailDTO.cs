using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain.Order
{
    public class OrderDetailDTO
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public string StatusShortName { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal ITBIS { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailItemDTO> Items { get; set; }
        public string Client { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class OrderDetailItemDTO
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public string Title { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ITBIS { get; set; }
        public decimal TotalAmount { get; set; }
        public string StatusShortName { get; set; }
        public string Status { get; set; }

        public bool UseStock { get; set; }
        public decimal Stock { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }
        public string Comment { get; set; }
        public long? ClientId { get; set; }
    }

    
    public class OrderInboxDTO
    {
        public long Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public long? ClientId { get; set; }
    }

    public class OrderStatusDTO
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }

}