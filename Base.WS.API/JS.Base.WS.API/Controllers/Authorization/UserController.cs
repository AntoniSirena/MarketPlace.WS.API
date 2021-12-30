using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.User;
using JS.Base.WS.API.DTO.SP_Parameter;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Authorization
{

    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : GenericApiController<Models.Authorization.User>
    {
        private UserService UserService;
        private MyDBcontext db;
        private Response response;

        public UserController()
        {
            UserService = new UserService();
            db = new MyDBcontext();
            response = new Response();
        }


        public override IHttpActionResult Create(dynamic entity)
        {
            string userName = entity["UserName"];
            if (userName.Contains(" "))
            {
                response.Code = "400";
                response.Message = "Estimado usuario el nombre de usuario no puede tener espacios el blancos, favor corregir el mismo y vuelva a intentarlo";

                return Ok(response);
            }

            string mail = entity["EmailAddress"];
            var userMail = db.Users.Where(x => x.EmailAddress == mail).FirstOrDefault();

            if (userMail != null)
            {
                response.Code = "400";
                response.Message = "El correo que intenta registrar ya esta asociado a una cuenta existente. Debes cambiar el mismo";

                return Ok(response);
            }

            string currentPassword = entity["Password"];
            entity["Password"] = Utilities.Security.Encrypt_OneWay(currentPassword);

            entity["Image"] = ConfigurationParameter.UserAvataDefault;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            var ValidateUser = db.Database.SqlQuery<ValidateUserName>(
               "Exec SP_ValidateUserName @UserName",
               new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Text, Value = (object)entity["UserName"].ToString() ?? DBNull.Value }
             ).ToList();

            if (ValidateUser[0].UserNameExist)
            {
                response.Code = "024";
                response.Message = "El nombre de usuario que desea registrar ya existe";
                return Ok(response);
            }

            return base.Create(input);

        }

        public override IHttpActionResult Update(dynamic entity)
        {
            object input = JsonConvert.DeserializeObject<object>(entity.ToString());

            if (string.IsNullOrEmpty(entity["StatusId"].ToString()))
            {
                response.Code = "444";
                response.Message = "Debe seleccionar un estado valido";
                return Ok(response);
            }

            return base.Update(input);
        }

        public override IHttpActionResult GetAll()
        {
            var Users = db.Users
                .Where(y => y.IsActive == true)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    Type = x.UserType.Description,
                    Name = x.Name,
                    Surname = x.Surname,
                    Status = x.UserStatus.Description,
                    StatusColor = x.UserStatus.Colour,
                    LastLoginTime = x.LastLoginTime,
                    LastLoginTimeEnd = x.LastLoginTimeEnd,
                    IsOnline = x.IsOnline,
                    Role = (from ur in db.UserRoles
                            where (x.Id == ur.UserId)
                            select (new RoleDto
                            {
                                Description = ur.Role.Description,
                                Parent = ur.Role.Parent
                            })).FirstOrDefault(),
                }).OrderByDescending(x => x.Id)
                  .ToList();

            return Ok(Users);
        }

        [HttpGet]
        [Route("GetUserStatuses")]
        public IHttpActionResult GetUserStatuses()
        {
            var result = UserService.GetUserStatuses();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserDetails/{userId}")]
        public IHttpActionResult GetUserDetails(long userId)
        {
            var result = UserService.GetUserDetails(userId);

            return Ok(result);
        }

    }
}
