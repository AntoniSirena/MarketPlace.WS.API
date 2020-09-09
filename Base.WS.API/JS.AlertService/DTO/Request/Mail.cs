using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AlertService.DTO.Request
{
    public class Mail
    {
        public string MailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
