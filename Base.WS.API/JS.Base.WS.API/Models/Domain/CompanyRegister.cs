using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class CompanyRegister: Audit
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Addreess { get; set; }
        public string PhoneNumber { get; set; }
        public string Schedule { get; set; }
        public int CategoryId { get; set; }
        public bool IsReviewed { get; set; }


        [ForeignKey("CategoryId")]
        public virtual CompanyCategory CompanyCategory { get; set; }
    }
}