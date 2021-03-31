using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class ArticleSubCategory : Audit
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DescriptionFormatted { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}