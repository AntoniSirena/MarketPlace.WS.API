using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.EnterpriseConf
{
    public class Enterprise : Audit
    {
        [Key]
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
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public string ImageContenTypeShort { get; set; }
        public string ImageContenTypeLong { get; set; }
        public bool AvailableOnlineAppointment { get; set; }
        public int ServiceTime { get; set; }
        public int NumberAppointmentsAttendedByDay { get; set; }
        public string EnterpriseDescription { get; set; }
        public int? ScheduleHourId { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ScheduleHourId")]
        public virtual ScheduleHour ScheduleHour { get; set; }
    }
}