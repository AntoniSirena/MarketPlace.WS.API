using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Permission
{
    public class UserRol: Audit
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}