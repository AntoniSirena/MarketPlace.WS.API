using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models
{
    public class Currency : Audit
    {
        [Key]
        public int Id { get; set; }
        public string Contry { get; set; }
        public string ISO_Currency { get; set; }
        public string ISO_Code { get; set; }
        public string ISO_Symbol { get; set; }
        public int ISO_Number { get; set; }
    }
}