using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class AppointmentDTO
    {
        public long Id { get; set; }
        public string EnterpriseName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }
        public string StatusColour { get; set; }
    }
}