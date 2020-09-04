using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.AccompInstDetail.Domain
{
    public class _VariableDetailsDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Variable { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }
        public string VariableDescription { get; set; }
        public string VariableTitle { get; set; }
        public List<_VariableDetail> VariableDetails { get; set; }
        public string AreaA { get; set; }
        public string AreaB { get; set; }
        public string AreaC { get; set; }

        //Efficiency data
        public string EfficiencyValueA { get; set; }
        public string EfficiencyColourA { get; set; }

        public string EfficiencyValueB { get; set; }
        public string EfficiencyColourB { get; set; }

        public string EfficiencyValueC { get; set; }
        public string EfficiencyColourC { get; set; }

        public string EfficiencyTotalValue { get; set; }
        public string EfficiencyTotalColour { get; set; }

        public string EfficiencyGeneralValue { get; set; }
        public string EfficiencyGeneralColour { get; set; }

        public string EfficiencyEvaluateFactor { get; set; }


        //Visit available
        public bool VisitAIsAvailable { get; set; }
        public bool VisitBIsAvailable { get; set; }
        public bool VisitCIsAvailable { get; set; }


        //Error
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }



    public class _VariableDetail
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string AreaA { get; set; }
        public string IndicadorA { get; set; }
        public string AreaB { get; set; }
        public string IndicadorB { get; set; }
        public string AreaC { get; set; }
        public string IndicadorC { get; set; }
    }
}