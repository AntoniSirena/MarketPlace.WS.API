using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Permission
{
    public class Role: Audit
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public string MenuTemplate { get; set; }
        public string Parent { get; set; }
        public bool Enabled { get; set; }
        public string Code { get; set; }
        public int? PersonTypeId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }


        [ForeignKey("PersonTypeId")]
        public virtual PersonType PersonType { get; set; }

    }
}