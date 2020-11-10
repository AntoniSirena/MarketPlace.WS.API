using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Alert.WS.API.DTO.Request.Alert
{
    public class SMS
    {
        public string Body { get; set; }
        public string PhoneNumber { get; set; }
    }
}