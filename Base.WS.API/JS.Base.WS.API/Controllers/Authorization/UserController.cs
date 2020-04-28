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


        public override IHttpActionResult GetAll()
        {
            var Users = db.Users
                .Where(y => y.IsActive == true)
                .Select(x => new
                                {
                                    Id = x.Id,
                                    UserName = x.UserName,
                                    EmailAddress = x.EmailAddress,
                                    Name = x.Name,
                                    Surname = x.Surname,
                                    Status = x.UserStatus.Description,
                                    Role = (from ur in db.UserRoles
                                            where (x.Id == ur.UserId)
                                            select (new Role
                                            {
                                                Description = ur.Role.Description,
                                                Parent = ur.Role.Parent
                                            })).FirstOrDefault(),
                                })
                                .OrderByDescending(x => x.Id)
                                .ToList();

            return Ok(Users);
        }



       public class Role
        {
            public string Description { get; set; }
            public string Parent { get; set; }
        }
    }
}
