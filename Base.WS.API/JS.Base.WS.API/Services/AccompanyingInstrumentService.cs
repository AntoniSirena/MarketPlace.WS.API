using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class AccompanyingInstrumentService : IAccompanyingInstrumentService
    {
        private MyDBcontext db;

        public AccompanyingInstrumentService()
        {
            db = new MyDBcontext();
        }

        public List<AccompInstRequestDto> GetAccompInstRequest()
        {
            var result = new List<AccompInstRequestDto>();

            result = db.AccompanyingInstrumentRequests.Select(x => new AccompInstRequestDto()
            {
                Id = x.Id,
                DocentFullName = x.Docent.FullName,
                Status = x.RequestStatu.Name,
                StatusColour = x.RequestStatu.Colour,
                OpeningDate = x.OpeningDate.ToString(),
                ClosingDate = x.ClosingDate.ToString(),
                AllowEdit = x.RequestStatu.AllowEdit,

            }).OrderByDescending(x => x.Id)
            .ToList();

            return result;

        }
    }
}