using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class VariableDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Variable { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }
        public string VariableDescription { get; set; }
        public string VariableTitle { get; set; }
        public List<VariableDetailsDto> VariableDetails { get; set; }
        public int AreaIdA { get; set; }
        public int AreaIdB { get; set; }


        public int AreaIdC { get; set; }

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

        //Error
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class VariableDetailsDto
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public int AreaIdA { get; set; }
        public int IndicadorIdA { get; set; }
        public int AreaIdB { get; set; }
        public int IndicadorIdB { get; set; }
        public int AreaIdC { get; set; }
        public int IndicadorIdC { get; set; }
    }
}