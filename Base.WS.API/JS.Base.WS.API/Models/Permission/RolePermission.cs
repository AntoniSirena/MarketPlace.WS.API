using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Permission
{
    public class RolePermission: Audit
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int EntityActionId { get; set; }
        public bool HasPermission { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("EntityActionId")]
        public virtual EntityAction EntityAction { get; set; }
    }
}