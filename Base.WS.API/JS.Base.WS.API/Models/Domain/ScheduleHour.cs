using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class ScheduleHour
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool ShowToCustomer { get; set; }
    }
}