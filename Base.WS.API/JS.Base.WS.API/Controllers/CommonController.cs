using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.DTO.Response;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.DTO.Response.Person;
using JS.Base.WS.API.Global;
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
        long currentUserId = CurrentUser.GetId();

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
            var result = db.DocumentTypes.Where(x => x.IsActive == true).Select(y => new DocumentTypeDto
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
            var result = db.Regionals.Where(x => x.IsActive == true).Select(y => new RegionalDto
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
            var result = db.Districts.Where(x => x.IsActive == true).Select(y => new DistrictDto
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
            var result = db.Areas.Where(x => x.IsActive == true).Select(y => new AreaDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetCurrentUserInfo")]
        public IHttpActionResult GetCurrentUserInfo()
        {
            var user = db.Users.Where(x => x.Id == currentUserId && x.IsActive == true && x.UserStatus.ShortName == Constants.UserStatuses.Active).FirstOrDefault();

            var result = new CurrentUserInfoDto();

            if (user.Person == null)
            {
                result.FirstName = user.Person.FirstName;
                result.SecondName = user.Person.SecondName;
                result.Surname = user.Person.Surname;
                result.SecondSurname = user.Person.secondSurname;
                result.FullName = user.Person.FullName;
                result.BirthDate = user.Person.BirthDate;
                result.DocumentType = user.Person.DocumentType.Description;
                result.DocumentNumber = user.Person.DocumentNumber;
            }
            else
            {
                result.FirstName = string.Empty;
                result.SecondName = string.Empty;
                result.Surname = string.Empty;
                result.SecondSurname = string.Empty;
                result.FullName = string.Empty;
                result.BirthDate = string.Empty;
                result.DocumentType = string.Empty;
                result.DocumentNumber = string.Empty;
            }

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
