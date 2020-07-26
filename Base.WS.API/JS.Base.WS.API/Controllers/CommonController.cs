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
        [Route("GetDistrictByRegionalId/{regionalId}")]
        public IHttpActionResult GetDistrictByRegionalId(int regionalId)
        {
            var result = db.Districts.Where(x => x.IsActive == true && x.RegionalId == regionalId).Select(y => new DistrictDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetEducativeCenterByDistrictId/{districtId}")]
        public IHttpActionResult GetEducativeCenterByDistrictId(int districtId)
        {
            var result = db.EducativeCenters.Where(x => x.IsActive == true && x.DistrictId == districtId).Select(y => new EducativeCenterDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetEducativeCenters")]
        public IHttpActionResult GetEducativeCenters()
        {
            var result = db.EducativeCenters.Where(x => x.IsActive == true).Select(y => new EducativeCenterDto
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
        [Route("GetCurrentUserInfo/{userId}")]
        public IHttpActionResult GetCurrentUserInfo(long userId = 0)
        {
            var user = new User();
            if (userId == 0)
            {
                user = db.Users.Where(x => x.Id == currentUserId && x.IsActive == true && x.UserStatus.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            }
            else
            {
                user = db.Users.Where(x => x.Id == userId && x.IsActive == true && x.UserStatus.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            }

            var result = new CurrentUserInfoDto();

            if (user != null)
            {
                if (user.Person != null)
                {
                    result.Id = user.Id;
                    result.FirstName = user.Person.FirstName;
                    result.SecondName = user.Person.SecondName;
                    result.Surname = user.Person.Surname;
                    result.SecondSurname = user.Person.secondSurname;
                    result.FullName = user.Person.FullName;
                    result.BirthDate = user.Person.BirthDate;
                    result.DocumentType = user.Person.DocumentType == null ? string.Empty : user.Person.DocumentType.Description;
                    result.DocumentNumber = user.Person.DocumentNumber;
                }
                else
                {
                    result.Id = 0;
                    result.FirstName = string.Empty;
                    result.SecondName = string.Empty;
                    result.Surname = string.Empty;
                    result.SecondSurname = string.Empty;
                    result.FullName = string.Empty;
                    result.BirthDate = string.Empty;
                    result.DocumentType = string.Empty;
                    result.DocumentNumber = string.Empty;
                }
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("GetIndicators")]
        public IHttpActionResult GetIndicators()
        {
            var result = db.Indicators.Where(x => x.IsActive == true).Select(y => new IndicatorDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetTandas")]
        public IHttpActionResult GetTandas()
        {
            var result = db.Tandas.Where(x => x.IsActive == true).Select(y => new TandaDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetGrades")]
        public IHttpActionResult GetGrades()
        {
            var result = db.Grades.Where(x => x.IsActive == true).Select(y => new GradeDto
            {
                Id = y.Id,
                ShortName = y.ShortName,
                Name = y.Name,
                Description = y.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetDocents")]
        public IHttpActionResult GetDocents()
        {
            var result = db.Docents.Where(x => x.IsActive == true).Select(y => new DocentDto
            {
                Id = y.Id,
                FullName = y.FullName,
                DocumentNumber = y.DocumentNumber,

            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetDocentsByEducativeCenter/{educativeCenterId}")]
        public IHttpActionResult GetDocentsByEducativeCenter(int educativeCenterId)
        {
            var result = db.Docents.Where(x => x.IsActive == true && x.EducativeCenterId == educativeCenterId).Select(y => new DocentDto
            {
                Id = y.Id,
                FullName = y.FullName,
                DocumentNumber = y.DocumentNumber,

            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetDocentById/{id}")]
        public IHttpActionResult GetDocentById(long? id)
        {

            //Verifica si el id pertenece a una solicitud
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == id).FirstOrDefault();

            if (request != null)
            {
                id = request.DocentId;
            }

            var result = db.Docents.Where(x => x.Id == id).Select(y => new DocentDto
            {
                Id = y.Id,
                FullName = y.FullName,
                DocumentNumber = y.DocumentNumber,

            }).FirstOrDefault();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetVisits")]
        public IHttpActionResult GetVisits()
        {
            var result = db.Visits.Where(x => x.IsActive == true).Select(y => new TandaDto
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
