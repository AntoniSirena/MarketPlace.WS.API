using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.PersonProfile
{
    public class Locator : Audit
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int LocatorTypeId { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("LocatorTypeId")]
        public virtual LocatorType LocatorType { get; set; }
    }
}