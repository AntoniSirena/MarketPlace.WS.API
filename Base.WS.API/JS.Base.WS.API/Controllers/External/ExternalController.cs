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
    [Authorize]
    [RoutePrefix("api/external")]
    public class ExternalController : ApiController
    {
        MyDBcontext db = new MyDBcontext();

        [HttpPost]
        [Route("CreateUser")]
        public Response CreateUser(User user)
        {
            var ValidateUser = db.Database.SqlQuery<ValidateUserName>(
               "Exec SP_ValidateUserName @UserName",
               new SqlParameter() { ParameterName = "@UserName", SqlDbType = System.Data.SqlDbType.Text, Value = (object)user.UserName ?? DBNull.Value }
             ).ToList();

            if (ValidateUser[0].UserNameExist)
            {
                throw new ArgumentException("El nombre de usuario que desea registrar ya existe");
            }

            Response response = new Response();

            var PendigToActive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToActive).FirstOrDefault();

            user.StatusId = PendigToActive.Id;
            user.CreationTime = DateTime.Now;
            user.CreatorUserId = CurrentUser.GetId();
            user.IsActive = true;

            db.Users.Add(user);
            db.SaveChanges();

            response.Message = "Usuario credo con exito";

            return response; 
        }
    }
}
