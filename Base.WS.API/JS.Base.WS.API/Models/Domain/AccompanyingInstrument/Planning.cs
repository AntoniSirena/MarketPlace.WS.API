using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Planning: Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long RequestId { get; set; }

        [Required]
        public int StatusId { get; set; }


        [ForeignKey("RequestId")]
        public virtual AccompanyingInstrumentRequest Request { get; set; }

        [ForeignKey("StatusId")]
        public virtual RequestStatus Status { get; set; }


        public virtual ICollection<PlanningDetail> PlanningDetails { get; set; }
    }
}