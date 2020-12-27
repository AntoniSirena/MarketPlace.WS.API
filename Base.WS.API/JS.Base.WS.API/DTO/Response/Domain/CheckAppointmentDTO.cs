using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class CheckAppointmentDTO
    {
        public List<AppointmentId> AppointmentId { get; set; }
        public string Message { get; set; }
        public long Id { get; set; }
        public string EnterpriseImage { get; set; }
        public string EnterpriseName { get; set; }
        public string EnterprisePhoneNumber { get; set; }
        public string EnterpriseAddress { get; set; }
        public int EnterpriseServiceTime { get; set; }

        public int InFrontMe { get; set; }
        public string UserName { get; set; }
        public string DocumentNomber { get; set; }
        public long PhoneNumber { get; set; }
        public string Comment { get; set; }
        public string StartDate { get; set; }
    }


    public class AppointmentId
    {
        public long Id { get; set; }
    }
}