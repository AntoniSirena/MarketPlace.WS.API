using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO;
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

        [HttpGet]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromUri] UserRequest user)
        {

            if (user == null)
                throw new ArgumentException("La solictud no puede estar vacia");

            var statusAcitve = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            var statusInactive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Inactive).FirstOrDefault();

            var currentUser = db.Users.Where(x => x.IsActive == true
                              && (x.UserName == user.UserName || x.EmailAddress == user.EmailAddress) 
                              && x.Password == user.Password).FirstOrDefault();

            if (currentUser == null)
            {
                throw new ArgumentException("Usuario invalido");
            }

            if (currentUser?.StatusId == statusInactive.Id)
            {
                throw new ArgumentException("Este usuarion esta inactivo");
            }

            if (currentUser != null)
            {
                string userParam = currentUser.UserName + "," + currentUser.Id.ToString();
                var token = TokenGenerator.GenerateTokenJwt(userParam);

                List<Locators> userLocators = db.Locators.Where(x => x.PersonId == currentUser.PersonId && x.IsActive == true).Select(x => new Locators
                {
                    Description = x.Description,
                    IsMain = x.IsMain,
                    Type = x.LocatorType.Description,
                }).ToList();


                Profile profile = new Profile
                {
                    user = new DTO.User
                    {
                        Id = currentUser.Id,
                        UserName = currentUser.UserName,
                        Name = currentUser.Name,
                        Surname = currentUser.Surname,
                        EmailAddress = currentUser.EmailAddress,
                        Image = currentUser.Image,
                        Token = token,
                    },
                    person = new Person
                    {
                        FirstName = currentUser.Person.FirstName,
                        SecondName = currentUser.Person.SecondName,
                        Surname = currentUser.Person.Surname,
                        secondSurname = currentUser.Person.secondSurname,
                        BirthDate = currentUser.Person.BirthDate,
                        FullName = currentUser.Person.FullName,
                        Gender = currentUser.Person.Gender.Description,
                        Locators = userLocators.Count == 0 ? new List<Locators>() : userLocators,
                    }                    
                    
                };              
               

                return Ok(profile);
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
