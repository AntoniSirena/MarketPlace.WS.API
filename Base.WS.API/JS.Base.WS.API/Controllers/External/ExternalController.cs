using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.DTO.SP_Parameter;
using System.Data.SqlClient;
using JS.Base.WS.API.DTO.Request;

namespace JS.Base.WS.API.Controllers.External
{
    [RoutePrefix("api/external")]
    public class ExternalController : ApiController
    {
        MyDBcontext db = new MyDBcontext();

        [HttpPost]
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(User user)
        {
            Response response = new Response();

            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.EmailAddress) || 
               string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname) )
            {
                response.Message = "Los Datos ingresados no son validos";
                response.Code = "007";
            }

            var ValidateUserName = db.Database.SqlQuery<ValidateUserName>(
               "Exec SP_ValidateUserName @UserName",
               new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Text, Value = (object)user.UserName ?? DBNull.Value }
             ).ToList();

            if (ValidateUserName[0].UserNameExist)
            {
                response.Message = "El nombre de usuario que desea registrar ya existe";
                response.Code = "001";
                
                return Ok(response);
            }

            var PendigToActive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToActive).FirstOrDefault();

            var systemUser = db.Users.Where(x => x.UserName == "system").FirstOrDefault();

            user.StatusId = PendigToActive.Id;
            user.CreationTime = DateTime.Now;
            user.CreatorUserId = systemUser.Id;
            user.IsActive = true;

            db.Users.Add(user);
            db.SaveChanges();

            response.Message = "Usuario creado con exito";

            return Ok(response);
        }
    }
}
