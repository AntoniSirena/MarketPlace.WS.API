using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.RequestFlowAI
{
    public class ResearchSummary: Audit
    {
        [Key]
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Summary { get; set; }


        [ForeignKey("RequestId")]
        public virtual AccompanyingInstrumentRequest Request { get; set; }
    }
}