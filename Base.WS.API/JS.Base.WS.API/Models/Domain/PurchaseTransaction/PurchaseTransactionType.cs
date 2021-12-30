using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.PurchaseTransaction
{
    public class PurchaseTransactionType: Audit
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}