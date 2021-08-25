using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.PurchaseTransaction
{
    public class PurchaseTransactionDetail : Audit
    {
        [Key]
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public long ArticleId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public long ProviderId { get; set; }

        public string Comment { get; set; }

        [ForeignKey("TransactionId")]
        public virtual PurchaseTransaction Transaction { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Market Article { get; set; }
    }
}