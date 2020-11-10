using JS.Alert.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Alert.WS.API.Models
{
    public class Operation: Audit
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Body { get; set; }
        public bool IsEnabled { get; set; }
    }
}