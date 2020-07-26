using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class AccompInstRequestDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string DocentFullName { get; set; }
        public string DocumentNumber { get; set; }
        public string Status { get; set; }
        public string StatusColour { get; set; }
        public string OpeningDate { get; set; }
        public string ClosingDate { get; set; }
        public bool AllowEdit { get; set; }

        public string EfficiencyGeneralValue { get; set; }
        public string EfficiencyGeneralColour { get; set; }
    }
}