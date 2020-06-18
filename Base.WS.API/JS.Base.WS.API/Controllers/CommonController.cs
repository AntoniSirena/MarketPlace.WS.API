using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.DTO.Response;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.DTO.Response.Person;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Models.PersonProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers
{
    [RoutePrefix("api/common")]
    [Authorize]
    public class CommonController : ApiController
    {
        MyDBcontext db = new MyDBcontext();
        long currenntUserId = CurrentUser.GetId();

        [HttpGet]
        [Route("GetGenders")]
        public IHttpActionResult GetGenders()
        {
            var result = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPesonTypes")]
        public IHttpActionResult GetPesonTypes()
        {
            var result = db.PersonTypes.Where(x => x.IsActive == true).Select(x => new PersonType
            {

                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetDocumentTypes")]
        public IHttpActionResult GetDocumentTypes()
        {
            var result = db.DocumentTypes.Where(x => x.IsActive == true).Select(y => new DocumentType
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetRegionals")]
        public IHttpActionResult GetRegionals()
        {
            var result = db.Regionals.Where(x => x.IsActive == true).Select(y => new Regional_Dto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDistricts")]
        public IHttpActionResult GetDistricts()
        {
            var result = db.Districts.Where(x => x.IsActive == true).Select(y => new District_Dto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetAreas")]
        public IHttpActionResult GetAreas()
        {
            var result = db.Areas.Where(x => x.IsActive == true).Select(y => new Area_Dto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }

        #region Models

        public class InfoCurrentUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
            public string EmailAddress { get; set; }
        }

        public class InfoCurrentPerson
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string SurName { get; set; }
            public string SecondSurname { get; set; }
            public string BirthDate { get; set; }
            public string FullName { get; set; }
            public int GenderId { get; set; }
        }

        #endregion

    }
}
