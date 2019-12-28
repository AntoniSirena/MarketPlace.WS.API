using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Permission
{
    public class EntityAction: Audit
    {
        [Key]
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Action { get; set; }

        [ForeignKey("EntityId")]
        public virtual Entity Entity { get; set; }
    }
}