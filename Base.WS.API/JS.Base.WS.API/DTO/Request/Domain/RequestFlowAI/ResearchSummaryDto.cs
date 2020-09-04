using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Request.Domain.RequestFlowAI
{
    public class ResearchSummaryDto
    {
        public long RequestId { get; set; }
        public string Summary { get; set; }
    }
}