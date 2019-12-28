using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Models.PersonProfile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/person")]
    public class PersonController: GenericApiController<Person>
    {
        public override IHttpActionResult Create(dynamic entity)
        {
            entity.FullName = entity.FirstName + " " + entity.SecondName + " " + entity.Surname + " " + entity.secondSurname;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            return base.Create(input);
        }

        public override IHttpActionResult Update(dynamic entity)
        {
            entity.FullName = entity.FirstName + " " + entity.SecondName + " " + entity.Surname + " " + entity.secondSurname;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            return base.Update(input);
        }
    }
}
