using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        MyDBcontext db = new MyDBcontext();
        long currenntUserId = CurrentUser.GetId();

        [HttpGet]
        [Route("GetGenders")]
        public List<GenderDto> GetGenders()
        {
            List<GenderDto> genders = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return genders;
        }

        [HttpGet]
        [Route("GetInfoCurrentUser")]
        public IHttpActionResult GetInfoCurrentUser()
        {
            var result = db.Users.Where(x => x.Id == currenntUserId).Select(x => new
            {
                UserName = x.UserName,
                Password = x.Password,
                Name = x.Name,
                Surname = x.Surname,
                EmailAddress = x.EmailAddress,
                Image = x.Image
            }).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetInfoCurrentPerson")]
        public IHttpActionResult GetInfoCurrentPerson()
        {
            var currentUser = db.Users.Where(x => x.Id == currenntUserId).FirstOrDefault();

            if (currentUser.PersonId != null)
            {
                var result = db.People.Where(x => x.Id == currentUser.PersonId).Select(x => new
                {
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    secondSurname = x.secondSurname,
                    BirthDate = x.BirthDate,
                    FullName = x.FullName,
                    GenderId = x.GenderId,
                }).FirstOrDefault();

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetInfoCurrentLocators")]
        public IHttpActionResult GetInfoCurrentLocators()
        {
            var currentUser = db.Users.Where(x => x.Id == currenntUserId).FirstOrDefault();

            if (currentUser.PersonId != null)
            {
                var result = db.Locators.Where(x => x.PersonId == currentUser.PersonId).Select(x => new
                {
                    LocatorTypeId = x.LocatorTypeId,
                    LocatorTypeDescription = x.LocatorType.Description,
                    Description = x.Description,
                    IsMain = x.IsMain
                }).ToList();

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        #region Models

        #endregion
    }
}
