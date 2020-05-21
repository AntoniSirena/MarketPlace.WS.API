using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO;
using JS.Base.WS.API.DTO.Response.User;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Services;
using JS.Base.WS.API.Templates;
using Newtonsoft.Json;
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
        private MyDBcontext db;
        private ConfigurationParameterService ConfigurationParameterService;

        public LoginController()
        {
            db = new MyDBcontext();
            ConfigurationParameterService = new ConfigurationParameterService();
        }

        [Authorize]
        [HttpGet]
        [Route("readyToRequest")]
        public IHttpActionResult ReadyToRequest()
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
            Response response = new Response();
            UserResponse userResponse = new UserResponse();

            if (user == null)
            {
                response.Code = "002";
                response.Message = "La solictud no puede estar vacia";

                return Ok(response);
            }

            var statusAcitve = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            var statusInactive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Inactive).FirstOrDefault();
            var PendigToActive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToActive).FirstOrDefault();

            var currentUser = db.Users.Where(x => x.IsActive == true
                              && (x.UserName == user.UserName || x.EmailAddress == user.EmailAddress) 
                              && x.Password == user.Password).FirstOrDefault();

            if (currentUser == null)
            {
                response.Code = "003";
                response.Message = "Credenciales invalida";

                return Ok(response);
            }

            if (currentUser?.StatusId == statusInactive.Id)
            {
                response.Code = "003";
                response.Message = "Usuario inactivo";

                return Ok(response);
            }

            if (currentUser?.StatusId == PendigToActive.Id)
            {
                response.Code = "005";
                response.Message = "Usuario pendiente de activar";

                return Ok(response);
            }

            if (currentUser != null)
            {
                string userParam = currentUser.UserName + "," + currentUser.Id.ToString();
                var token = TokenGenerator.GenerateTokenJwt(userParam);

                var userRole  = db.UserRoles.Where(x => x.UserId == currentUser.Id && x.IsActive == true).FirstOrDefault();

                //Permissions
                if (userRole != null)
                {
                    DTO.Response.User.Permission permissionResponse = new DTO.Response.User.Permission();

                    var permission = db.Entities.Where(x => x.IsActive == true).Select(x => new Entity
                      {
                       Id = x.Id,
                        Description = x.Description,
                        ShortName = x.ShortName,
                        EntityActions = (from perm in db.RolePermissions
                                         join entAct in db.EntityActions on perm.EntityActionId equals entAct.Id
                                         where perm.RoleId == userRole.RoleId && x.Id == entAct.EntityId
                                         select new EntityActions
                                         { 
                                             Id = entAct.Id,
                                             ActionName = entAct.Action,
                                             HasPermissio = perm.HasPermission
                                         }).ToList(),
                    }).ToList();

                    permissionResponse.Entities = permission;

                    userResponse.Permissions = permissionResponse;
                }
                else
                {
                    response.Code = "006";
                    response.Message = "Este usuario no tiene un rol asignado";

                    return Ok(response);
                }
                //End Permissions


                //Profile
                List<Locators> userLocators = db.Locators.Where(x => x.PersonId == currentUser.PersonId && x.IsActive == true).Select(x => new Locators
                {
                    Description = x.Description,
                    IsMain = x.IsMain,
                    Type = x.LocatorType.Description,
                }).ToList();

                Profile profile = new Profile
                {
                    User = new DTO.Response.User.User
                    {
                        Id = currentUser.Id,
                        UserName = currentUser.UserName,
                        Name = currentUser.Name,
                        Surname = currentUser.Surname,
                        EmailAddress = currentUser.EmailAddress,
                        Image = currentUser.Image,
                        Token = "Bearer " + token,
                        WelcomeMessage = currentUser.Name + " " + currentUser.Surname + ", " + "sea bienvenido al sistema",
                        MenuTemplate = string.Empty,
                        RoleDescription = userRole.Role.Description,
                        RoleShortName = userRole.Role.ShortName,
                        RoleParent = userRole.Role.Parent,
                    },
                    Person = currentUser.Person == null ? new Person() : new Person
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

                //Get menu template
                var menu = db.UserRoles
                              .Where(x => x.UserId == currentUser.Id && x.Role.Enabled == true)
                              .Select(x => x.Role.MenuTemplate)
                              .FirstOrDefault();

                if (menu != null)
                {
                    profile.User.MenuTemplate = menu;
                }

                userResponse.Profile = profile;
                //End Profile

                //System configuration
                string configuration = Constants.ConfigurationParameter.SystemConfigurationTemplate;
                if (configuration != null)
                {
                    var resulConfiguration = JsonConvert.DeserializeObject<Configuration>(configuration);
                    userResponse.Configuration = resulConfiguration;
                }
                else
                {
                    userResponse.Configuration = null;
                }

                //End System configuration

                response.Message = "Usuario autenticado con exito.";
                response.Data = userResponse;

                return Ok(response);
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
