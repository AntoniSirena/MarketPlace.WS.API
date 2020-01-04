using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Models.PersonProfile;
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
        public List<GenderResponse> getGenders()
        {
            List<GenderResponse> genders = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderResponse
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return genders;
        }



        public class GenderResponse
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string ShortName { get; set; }
        }
    }
}
