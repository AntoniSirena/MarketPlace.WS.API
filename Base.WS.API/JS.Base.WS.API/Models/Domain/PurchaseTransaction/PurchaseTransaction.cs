﻿using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JS.Base.WS.API.Models.Domain.PurchaseTransaction
{
    public class PurchaseTransaction: Audit
    {
        [Key]
        public long Id { get; set; }
        public int StatusId { get; set; }
        public int TransactionTypeId { get; set; }
        public int? PaymentMethodId { get; set; }
        public long UserId { get; set; }
        public int CurrencyISONumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountPending { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public string FormattedDate { get; set; }
        public string Key { get; set; }

        public DateTime? DeliverDate { get; set; }
        public long? DeliveredByUser { get; set; }

        [ForeignKey("StatusId")]
        public virtual PurchaseTransactionStatus Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User Client { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual PurchaseTransactionType TransactionType { get; set; }

         [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual ICollection<PurchaseTransactionDetail> ArticlesDetails { get; set; }

    }
}