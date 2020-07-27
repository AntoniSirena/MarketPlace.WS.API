using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class AccompanyingInstrumentRequest : Audit
    {
        [Key]
        public long Id { get; set; }
        public int StatusId { get; set; }
        public long? DocentId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string EfficiencyGeneralValue { get; set; }
        public string EfficiencyGeneralColour { get; set; }
        public bool VisitAIsAvailable { get; set; }
        public bool VisitBIsAvailable { get; set; }
        public bool VisitCIsAvailable { get; set; }

        [ForeignKey("StatusId")]
        public virtual RequestStatus RequestStatu { get; set; }

        [ForeignKey("DocentId")]
        public virtual Docent Docent { get; set; }
    }
}