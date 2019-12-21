using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Authorization
{

    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        MyDBcontext db = new MyDBcontext();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(UserRequest user)
        {

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var statusAcitve = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            var statusInactive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Inactive).FirstOrDefault();

            var currentUser = db.Users.Where(x => x.IsActive == true
                              && (x.UserName == user.UserName || x.EmailAddress == user.EmailAddress) 
                              && x.Password == user.Password).FirstOrDefault();

            if (currentUser?.StatusId == statusInactive.Id)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
                //throw new ArgumentException("Este usuarion esta inactivo", "0001");
            }

            if (currentUser?.Password == user.Password)
            {
                string userParam = currentUser.UserName + "," + currentUser.Id.ToString();
                var token = TokenGenerator.GenerateTokenJwt(userParam);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }


        public class UserRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
