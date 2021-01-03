using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Enterprise
{
    public class EnterpriseDto: Audit
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string PropetaryName { get; set; }
        public string Name { get; set; }
        public string RNC { get; set; }
        public string CommercialRegister { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Sigla { get; set; }
        public string Slogan { get; set; }
        public string WorkSchedule { get; set; }
        public bool AvailableOnlineAppointment { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public string ImageContenTypeShort { get; set; }
        public string ImageContenTypeLong { get; set; }
        public int ServiceTime { get; set; }
        public int NumberAppointmentsAttendedByDay { get; set; }
        public string EnterpriseDescription { get; set; }
        public int? ScheduleHourId { get; set; }
        public double ScheduleHourValue { get; set; }
        public int? ScheduleHourCloseId { get; set; }
        public double ScheduleHourCloseValue { get; set; }

    }
}