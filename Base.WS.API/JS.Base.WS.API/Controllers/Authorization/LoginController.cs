using JS.AlertService.DTO.Request;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Authorization
{

    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private MyDBcontext db;
        private ConfigurationParameterService ConfigurationParameterService;
        private UserService UserService;
        private Response response;


        public LoginController()
        {
            db = new MyDBcontext();
            ConfigurationParameterService = new ConfigurationParameterService();
            UserService = new UserService();
            response = new Response();
        }

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
            UserResponse userResponse = new UserResponse();

            string secondFactorAuthentication = Constants.ConfigurationParameter.SecondFactorAuthentication;
            secondFactorAuthentication = secondFactorAuthentication.ToUpper();

            string currentSecuryCode = string.Empty;
            var currentUser = new Models.Authorization.User();

            var statusAcitve = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            var statusInactive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Inactive).FirstOrDefault();
            var pendigToActive = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToActive).FirstOrDefault();
            var pendingToChangePassword = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToChangePassword).FirstOrDefault();

            string encryptPassword = Utilities.Security.Encrypt_OneWay(user.Password);

            var date_2FA_ExpirationTime = new DateTime();


            if (user == null)
            {
                response.Code = "002";
                response.Message = "La solictud debe contener datos válidos, para ser procesada";

                return Ok(response);
            }


            //Decrypt Token2FA
            if (secondFactorAuthentication.Equals("TRUE") & (!string.IsNullOrEmpty(user.SecurityCode) & !string.IsNullOrEmpty(user.Token2AF)))
            {
                user.Token2AF = HttpUtility.UrlDecode(user.Token2AF);
                string decryptToken2FA = Utilities.Security.Decrypt_TwoWay(user.Token2AF);

                string[] arrayToken2AF = decryptToken2FA.Split(',');

                user.UserName = arrayToken2AF[0];
                user.Password = arrayToken2AF[1];
                currentSecuryCode = arrayToken2AF[2];
                date_2FA_ExpirationTime = Convert.ToDateTime(arrayToken2AF[3]);
            }

            if (!string.IsNullOrEmpty(user.Token2AF) & !string.IsNullOrEmpty(user.SecurityCode))
            {
                currentUser = db.Users.Where(x => x.IsActive == true
                  && (x.UserName == user.UserName || x.EmailAddress == user.UserName)
                  && x.Password == user.Password).FirstOrDefault();
            }
            else
            {
                currentUser = db.Users.Where(x => x.IsActive == true
                  && (x.UserName == user.UserName || x.EmailAddress == user.UserName)
                  && x.Password == encryptPassword).FirstOrDefault();
            }

            if (currentUser == null)
            {
                response.Code = "003";
                response.Message = "Credenciales inválida";

                return Ok(response);
            }

            if (currentUser?.StatusId == statusInactive.Id)
            {
                response.Code = "003";
                response.Message = "Usuario inactivo";

                return Ok(response);
            }

            if (currentUser?.StatusId == pendigToActive.Id)
            {
                response.Code = "005";
                response.Message = "Usuario pendiente de activar";

                return Ok(response);
            }

            if (currentUser?.StatusId == pendingToChangePassword.Id)
            {
                response.Code = "005";
                response.Message = "Usuario pendiente de cambiar contraseña. Favor confirme el correo que ha recibido en su bandeja de entrada o SMS";

                return Ok(response);
            }


            //Validate 2FA second factor authentication 
            #region 2FA

            if (secondFactorAuthentication.Equals("TRUE") & !currentUser.IsVisitorUser & !user.RefreshToken)
            {
                if (string.IsNullOrEmpty(user.SecurityCode) & string.IsNullOrEmpty(user.Token2AF))
                {
                    string url_SecondFactorAuthentication = Constants.ConfigurationParameter.URL_SecondFactorAuthentication;

                    string securityCode = Utilities.Security.GenerateSecurityCode(6);

                    int securityCode_ExpirationTime = Convert.ToInt32(Constants.ConfigurationParameter.SecurityCode_ExpirationTime_SecondFactorAuthentication);

                    string token2FA = string.Concat(user.UserName, ",", Utilities.Security.Encrypt_OneWay(user.Password), ",", securityCode, ",", DateTime.Now.AddMinutes(securityCode_ExpirationTime).ToString() );
                    token2FA = Utilities.Security.Encrypt_TwoWay(token2FA);

                    string url2FA = string.Concat(url_SecondFactorAuthentication, "/", HttpUtility.UrlEncode(token2FA));

                    string sendEmailAlert2FA = Constants.ConfigurationParameter.SendEmailAlert_SecondFactorAuthentication;
                    sendEmailAlert2FA = sendEmailAlert2FA.ToUpper();

                    string sendSMSAlert2FA = Constants.ConfigurationParameter.SendSMSAlert_SecondFactorAuthentication;
                    sendSMSAlert2FA = sendSMSAlert2FA.ToUpper();


                    //Send Email Alert
                    #region Send Email Alert

                    string confirmation_Operation = AlertService.Alert.GetOperation("AccessConfirmation");
                    confirmation_Operation = confirmation_Operation.Replace("@UserName", currentUser.UserName);
                    confirmation_Operation = confirmation_Operation.Replace("@SecurityCode", securityCode);
                    confirmation_Operation = confirmation_Operation.Replace("@Time", securityCode_ExpirationTime.ToString());

                    var requestEmail = new Mail();
                    var responseEmail = new AlertService.Base.ClientResponse<bool>();

                    if (sendEmailAlert2FA.Equals("TRUE"))
                    {
                        //Validate email
                        if (string.IsNullOrEmpty(currentUser.EmailAddress))
                        {
                            response.Code = "005";
                            response.Message = string.Concat("Estimado ", currentUser.UserName, " usted no tiene un correo registrado, para recibir notificaciones");

                            return Ok(response);
                        }

                        requestEmail.MailAddresses = currentUser.EmailAddress;
                        requestEmail.Subject = "Confirmar acceso";
                        requestEmail.Body = confirmation_Operation;
                        
                        responseEmail = AlertService.Alert.SendMail(requestEmail);
                    }
                    #endregion


                    //Send SMS Alert
                    #region Send SMS Alert

                    var requestSMS = new SMS();
                    var responseSMS = new AlertService.Base.ClientResponse<bool>();

                    if (sendSMSAlert2FA.Equals("TRUE"))
                    {
                        //Validate phoneNumber
                        if (string.IsNullOrEmpty(currentUser.PhoneNumber) & !responseEmail.Data)
                        {
                            response.Code = "005";
                            response.Message = string.Concat("Estimado ", currentUser.UserName, " usted no tiene un número movil registrado, para recibir notificaciones");

                            return Ok(response);
                        }

                        if (!string.IsNullOrEmpty(currentUser.PhoneNumber))
                        {
                            requestSMS.Body = string.Concat("Saludo estimado ", currentUser.UserName, " su codigo de seguridad es: ", securityCode, " y expira en ", securityCode_ExpirationTime.ToString(),  " minutos.");
                            requestSMS.PhoneNumber = currentUser.PhoneNumber;

                            responseSMS = AlertService.Alert.SendSMS(requestSMS);
                        }
                    };
                    #endregion


                    if (responseEmail.Data  || responseSMS.Data)
                    {
                        return Content(HttpStatusCode.Redirect, url2FA);
                    }
                    else
                    {
                        response.Code = "005";
                        response.Message = "No se encontró un canal disponible, para enviar el código de seguridad";
                        return Ok(response);
                    }

                }


                if (!string.IsNullOrEmpty(user.SecurityCode) & !string.IsNullOrEmpty(user.Token2AF))
                {
                    if (!user.SecurityCode.Equals(currentSecuryCode))
                    {
                        response.Code = "005";
                        response.Message = "Código invalido, favor verifique el mismo ó vuelva a iniciar sesión";

                        return Ok(response);
                    }

                    if (DateTime.Now > date_2FA_ExpirationTime)
                    {
                        response.Code = "005";
                        response.Message = string.Concat("Estimado ", currentUser.UserName, " su código ha expirado, favor vuelva a iniciar sesión");

                        return Ok(response);
                    }
                }

            }

            #endregion


            if (currentUser != null)
            {
                int expireTime = 0;
                if (currentUser.IsVisitorUser)
                {
                    expireTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES_USER_VISITADOR"]);
                }
                else
                {
                    expireTime = Convert.ToInt32(Constants.ConfigurationParameter.LoginTime);
                }
                string lifeDate = DateTime.Now.AddMinutes(expireTime).ToString();
                string payLoad = currentUser.UserName + "," + currentUser.Id.ToString() + "," + lifeDate + "," + currentUser.IsVisitorUser.ToString();
                var token = TokenGenerator.GenerateTokenJwt(payLoad);

                var userRole = db.UserRoles.Where(x => x.UserId == currentUser.Id && x.IsActive == true).FirstOrDefault();

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
                        IsVisitorUser = currentUser.IsVisitorUser,

                        //Permissions

                        //Crud
                        CanEdit = userRole.Role.CanEdit,
                        CanDelete = userRole.Role.CanDelete,
                        CanCreate = userRole.Role.CanCreate,

                        //Enterprise
                        CanCreateEnterprise = userRole.Role.CanCreateEnterprise,
                        CanEditEnterprise = userRole.Role.CanEditEnterprise,
                        CanDeleteEnterprise = userRole.Role.CanDeleteEnterprise,

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
                var _userRole = db.UserRoles
                              .Where(x => x.UserId == currentUser.Id)
                              .FirstOrDefault();

                var currentRole = db.Roles.Where(x => x.ShortName == _userRole.Role.Parent && x.Enabled == true).FirstOrDefault();

                string menuTemplate = string.Empty;

                if (currentUser.UserType.ShortName.Equals(Constants.UserTypes.Person))
                {
                    menuTemplate = currentRole.MenuTemplate;
                }

                if (currentUser.UserType.ShortName.Equals(Constants.UserTypes.Enterprise))
                {
                    menuTemplate = currentRole.EnterpriseMenuTemplate;
                }

                if (!string.IsNullOrEmpty(menuTemplate))
                {
                    profile.User.MenuTemplate = JsonConvert.DeserializeObject<Object>(menuTemplate);
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

                response.Message = "Usuario autenticado con éxito";
                response.Data = userResponse;

                //Update user
                bool UpdateUserLogIn = UserService.UpdateUserLogInOut(true, user.UserName, 0);

                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost]
        [Route("logOut")]
        public IHttpActionResult logOut([FromBody] long userId)
        {
            //Update user
            bool UpdateUserLogIn = UserService.UpdateUserLogInOut(false, string.Empty, userId);

            return Ok(UpdateUserLogIn);
        }


        [HttpPost]
        [Route("resetPassword")]
        public IHttpActionResult ResetPassword(UserRequest request)
        {
            string ip = System.Configuration.ConfigurationManager.AppSettings["JS.Base.WS.API_IP"];
            var pendingToChangePassword = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToChangePassword).FirstOrDefault();


            var user = db.Users.Where(x => x.UserName == request.UserName & x.EmailAddress == request.EmailAddress).FirstOrDefault();

            if (user == null)
            {
                response.Code = "003";
                response.Message = "El nombre de usuario o el correo estan incorrecto, favor verificar los mismo";

                return Ok(response);
            }

            string parameters = string.Concat(user.Id.ToString(), ",", user.UserName);
            string url = string.Concat(ip, "api/login/confirmPassword?userName=", HttpUtility.UrlEncode(Utilities.Security.Encrypt_TwoWay(parameters)));

            user.StatusId = pendingToChangePassword.Id;
            db.SaveChanges();

            //Send alert
            #region Send alert

            string operationBody = AlertService.Alert.GetOperation("ResetPassword");
            operationBody = operationBody.Replace("@UserName", user.UserName);
            operationBody = operationBody.Replace("@Link", url);

            var requestAlert = new Mail
            {
                MailAddresses = request.EmailAddress,
                Subject = "Cambio de clave",
                Body = operationBody,
            };

            var responseAlert = AlertService.Alert.SendMail(requestAlert);
            #endregion

            response.Message = "Favor revise su correo y confirme el mensaje recibido";

            return Ok(response);
        }


        [HttpGet]
        [Route("confirmPassword")]
        public IHttpActionResult ConfirmPassword(string userName)
        {
            string urlConfirmPassword = string.Concat(Constants.ConfigurationParameter.URL_ConfirmPassword, "/", userName);

            return Redirect(urlConfirmPassword);
        }


        [HttpPost]
        [Route("updatePassword")]
        public IHttpActionResult UpdatePassword(UserRequest request)
        {
            request.UserName = HttpUtility.UrlDecode(request.UserName);
            request.UserName = Utilities.Security.Decrypt_TwoWay(request.UserName);

            var statusAcitve = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.Active).FirstOrDefault();
            var pendingToChangePassword = db.UserStatus.Where(x => x.ShortName == Constants.UserStatuses.PendingToChangePassword).FirstOrDefault();

            string[] userNameArray = request.UserName.Split(',');
            long userId = Convert.ToInt64(userNameArray[0]);
            string userName = userNameArray[1];

            var user = db.Users.Where(x => x.Id == userId & x.UserName == userName).FirstOrDefault();

            if (user.StatusId != pendingToChangePassword.Id)
            {
                response.Code = "005";
                response.Message = "Su usuario se encuentra en un estado que no permite cambiar la contraseña";
                return Ok(response);
            }

            user.Password = Utilities.Security.Encrypt_OneWay(request.Password);
            user.StatusId = statusAcitve.Id;
            db.SaveChanges();

            response.Message = "Su contraseña fué actualizada de forma correcta";

            return Ok(response);
        }



        public class UserRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string EmailAddress { get; set; }
            public string SecurityCode { get; set; }
            public string Token2AF { get; set; }
            public bool RefreshToken { get; set; }
        }
    }
}
