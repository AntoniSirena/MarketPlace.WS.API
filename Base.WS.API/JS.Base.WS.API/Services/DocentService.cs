using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class DocentService : IDocentService
    {
        private MyDBcontext db;

        public DocentService()
        {
            db = new MyDBcontext();
        }

        public DocentDetails_Dto GetDocentDetails(long docentId)
        {
            var response = new DocentDetails_Dto();

            var docent = db.Docents.Where(x => x.Id == docentId && x.IsActive == true).FirstOrDefault();
            var docentResult = new DocentPersonInfo()
            {
                FirstName = docent.FirstName,
                SecondName = docent.SecondName,
                Surname = docent.Surname,
                SecondSurname = docent.SecondSurname,
                FullName = docent.FullName,
                BirthDate = docent.BirthDate.ToString("dd/MM/yyyy"),
                DocumentType = docent.DocumentType.Description,
                DocumentNumber = docent.DocumentNumber,
                Phone = docent.Phone,
                Address = docent.Address,
                Area = docent.Area.Description,
            };
            response.DocentPersonInfo = docentResult;



            return response;
        }
    }
}