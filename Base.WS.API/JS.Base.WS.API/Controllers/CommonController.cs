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

    }
}
