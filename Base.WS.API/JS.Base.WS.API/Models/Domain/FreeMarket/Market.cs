using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Market : Audit
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }

        public int MarketTypeId { get; set; }
        public int ConditionId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public string Ubication { get; set; }
        public long? PhoneNumber { get; set; }

        public string Img { get; set; }
        public string ImgPath { get; set; }
        public string ContenTypeShort { get; set; }
        public string ContenTypeLong { get; set; }

        public string CreationDate { get; set; }


        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        [ForeignKey("MarketTypeId")]
        public virtual MarketType MarketType { get; set; }

        [ForeignKey("ConditionId")]
        public virtual ArticleCondition ArticleCondition { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ArticleCategory Category { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual ArticleSubCategory SubCategory { get; set; }
    }
}