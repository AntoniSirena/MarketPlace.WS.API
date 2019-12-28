using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.Models.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Authorization
{

    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : GenericApiController<User>
    {

        public override IHttpActionResult Create(dynamic entity)
        {
                object input = JsonConvert.DeserializeObject<object>(entity.ToString());

                dynamic Entities = GetAll();

                List<object> result = new List<object>();

                foreach (var item in Entities.Content)
                {
                    if (item.UserName == entity["UserName"].ToString())
                    {
                        result.Add(item);
                        break;
                    }
                }

                if (result.Count() > 0)
                {
                    throw new ArgumentException("El nombre de usuario que desea registrar ya existe");
                }

            return base.Create(input);

        }

        //public override IHttpActionResult Update(dynamic entity)
        //{
        //    object input = JsonConvert.DeserializeObject<object>(entity.ToString());

        //     dynamic Entities = GetAll();

        //     List<object> result = new List<object>();

        //     foreach (var item in Entities.Content)
        //     {
        //         if (item.UserName == entity["UserName"].ToString() && item.Id != Convert.ToInt64(entity["Id"].ToString()))
        //         {
        //             result.Add(item);
        //             break;
        //         }
        //     }

        //     if (result.Count() > 0)
        //     {
        //         throw new ArgumentException("El nombre de usuario que desea registrar ya existe");
        //     }

        //     return base.Update(input);
        //}
    }
}
