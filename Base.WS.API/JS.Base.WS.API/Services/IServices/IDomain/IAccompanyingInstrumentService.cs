using JS.Base.WS.API.DTO.Response.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Base.WS.API.Services.IServices
{
  public  interface IAccompanyingInstrumentService
    {
        List<AccompInstRequestDto> GetAccompInstRequest();
        bool CreateVariables(long requestId);
        VariableDto GetVariableByRequestId(long requestId, string variable);
    }
}
