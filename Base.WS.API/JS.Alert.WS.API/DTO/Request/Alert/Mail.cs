using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Alert.WS.API.DTO.Request.Alert
{
    public class Mail
    {
        public string MailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}