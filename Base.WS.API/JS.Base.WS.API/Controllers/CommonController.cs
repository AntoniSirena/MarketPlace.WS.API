using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        MyDBcontext db = new MyDBcontext();

        [HttpGet]
        [Route("getGenders")]
        public List<GenderDto> getGenders()
        {
            List<GenderDto> genders = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return genders;
        }


    }
}
