using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.EnterpriseConf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Appointment: Audit
    {
        [Key]
        public long Id { get; set; }
        public long EnterpriseId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string DocumentNomber { get; set; }
        public string PhoneNomber { get; set; }
        public string Comment { get; set; }
        public string StartDate { get; set; }
        public bool ScheduledAppointment { get; set; }


        [ForeignKey("EnterpriseId")]
        public virtual Enterprise Enterprise { get; set; }

        [ForeignKey("StatusId")]
        public virtual AppointmentStatus AppointmentStatus { get; set; }

    }
}