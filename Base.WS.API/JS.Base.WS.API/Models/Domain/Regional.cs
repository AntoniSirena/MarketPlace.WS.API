using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Regional: Audit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ShortName { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}