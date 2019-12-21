using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.PersonProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Authorization
{
    public class User: Audit
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public int StatusId { get; set; }
        public int? PersonId { get; set; }

        [ForeignKey("StatusId")]
        public virtual UserStatus UserStatus { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}