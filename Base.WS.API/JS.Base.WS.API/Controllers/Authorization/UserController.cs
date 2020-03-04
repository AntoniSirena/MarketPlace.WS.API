using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.SP_Parameter;
using JS.Base.WS.API.Models.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        MyDBcontext db = new MyDBcontext();

        Response response = new Response();

        public override IHttpActionResult Create(dynamic entity)
        {
            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            var ValidateUser = db.Database.SqlQuery<ValidateUserName>(
               "Exec SP_ValidateUserName @UserName",
               new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Text, Value = (object)entity["UserName"].ToString() ?? DBNull.Value }
             ).ToList();

            if (ValidateUser[0].UserNameExist)
            {
                //throw new ArgumentException("El nombre de usuario que desea registrar ya existe");

                response.Code = "024";
                response.Message = "El nombre de usuario que desea registrar ya existe";
                return Ok(response);
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
