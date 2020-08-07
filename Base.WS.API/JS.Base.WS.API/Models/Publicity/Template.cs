using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Publicity
{
    public class Template: Audit
    {
        [Key]
        public int Id { get; set; }
        public string Operation { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public bool Enabled { get; set; }
    }
}