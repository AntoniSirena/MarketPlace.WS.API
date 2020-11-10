using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.AccompanyingInstrument
{
    public class EvaluationProcess: Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long RequestId { get; set; }

        [Required]
        public int StatusId { get; set; }


        //Efficiency data
        public string EfficiencyValueA { get; set; }
        public string EfficiencyColourA { get; set; }

        public string EfficiencyValueB { get; set; }
        public string EfficiencyColourB { get; set; }

        public string EfficiencyValueC { get; set; }
        public string EfficiencyColourC { get; set; }

        public string EfficiencyTotalValue { get; set; }
        public string EfficiencyTotalColour { get; set; }

        public string EfficiencyEvaluateFactor { get; set; }


        [ForeignKey("RequestId")]
        public virtual AccompanyingInstrumentRequest Request { get; set; }

        [ForeignKey("StatusId")]
        public virtual RequestStatus Status { get; set; }


        public virtual ICollection<EvaluationProcessDetail> EvaluationProcessDetails { get; set; }
    }
}