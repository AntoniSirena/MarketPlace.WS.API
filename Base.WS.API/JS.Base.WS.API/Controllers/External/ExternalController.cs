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
using JS.Base.WS.API.Templates;
using Newtonsoft.Json;
using JS.Base.WS.API.Services;
using static JS.Base.WS.API.Global.Constants;
using JS.Base.WS.API.DTO.Response.Publicity;
using System.Text.RegularExpressions;

namespace JS.Base.WS.API.Controllers.External
{
    [RoutePrefix("api/external")]
    [AllowAnonymous]
    public class ExternalController : ApiController
    {
        private MyDBcontext db;
        private UserRoleService UserRoleService;
        private UserService userService;
        private Response response;

        public ExternalController()
        {
            db = new MyDBcontext();
            UserRoleService = new UserRoleService();
            userService = new UserService();
            response = new Response();
        }

        [HttpPost]
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(User user)
        {
            //Validate if required securityCodeExternaRegister
            string securityCodeExternaRegister = ConfigurationParameter.Required_SecurityCodeExternaRegister;
            if (securityCodeExternaRegister.Equals("1"))
            {
                if (string.IsNullOrEmpty(user.Code))
                {
                    response.Code = InternalResponseCodeError.Code308;
                    response.Message = InternalResponseCodeError.Message308;

                    return Ok(response);
                }
            }

            //Validate security code
            bool resultValidateSecCode = UserRoleService.ValidateSecurityCode(user.Code);
            if (!resultValidateSecCode)
            {
                if (securityCodeExternaRegister.Equals("0"))
                {
                    response.Code = InternalResponseCodeError.Code306;
                    response.Message = InternalResponseCodeError.Message306;
                }
                else
                {
                    response.Code = InternalResponseCodeError.Code307;
                    response.Message = InternalResponseCodeError.Message307;
                }

                return Ok(response);
            }


            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.EmailAddress) || 
               string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname) )
            {
                response.Message = "Los Datos ingresados no son validos";
                response.Code = "007";
            }

            var validateUserName = userService.ValidateUserName(user.UserName);
            if (validateUserName.UserNameExist)
            {
                response.Message = "El nombre de usuario que desea registrar ya existe";
                response.Code = "001";
                
                return Ok(response);
            }

            //Clear PhoneNumber
            Regex re = new Regex("[;\\\\/:*?\"<>|&' ._-]");
            user.PhoneNumber = re.Replace(user.PhoneNumber, "");

            string StatusExternalUser = ConfigurationParameter.StatusExternalUser;
            var CurrentStatus = db.UserStatus.Where(x => x.ShortName == StatusExternalUser).FirstOrDefault();

            var systemUser = db.Users.Where(x => x.UserName == "system").FirstOrDefault();

            user.Password = Utilities.Security.Encrypt_OneWay(user.Password);
            user.StatusId = CurrentStatus.Id;
            user.CreationTime = DateTime.Now;
            user.CreatorUserId = systemUser.Id;
            user.IsActive = true;

           var UserResult = db.Users.Add(user);
            db.SaveChanges();


            //Create rol
            #region Create rol

            bool UserRol = UserRoleService.CreateUserRol(UserResult.Id, user.Code);

            #endregion


            response.Message = "Usuario creado con exito";

            return Ok(response);
        }

        [HttpGet]
        [Route("GetEnterpriseInfo")]
        public IHttpActionResult GetEnterpriseInfo()
        {
            Response response = new Response();

            var enterpriseResult = Constants.ConfigurationParameter.SystemConfigurationTemplate;
            var configuration = JsonConvert.DeserializeObject<Configuration>(enterpriseResult);

            string cadena = JsonConvert.SerializeObject(configuration.Data.Enterprise);
            var enterprise = JsonConvert.DeserializeObject<Enterprise>(cadena.ToString());

            response.Data = enterprise;

            return Ok(response);
        }

        [HttpGet]
        [Route("GetValueRegisterButton")]
        public IHttpActionResult GetValueRegisterButton()
        {
            string result = ConfigurationParameter.EnableRegistrationButton;

            return Ok(result);
        }


        [HttpGet]
        [Route("GetTemplate")]
        public IHttpActionResult GetTemplate(string operation)
        {
            var result = new PortadaDto();
            var template = db.Templates.Where(x => x.Operation == operation && x.IsActive == true && x.Enabled == true).FirstOrDefault();

            if (template != null)
            {
                result.Body = template.Body == null ? "Información en proceso, para ser publicada" : template.Body;
                response.Data = result;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code318;
                response.Message = InternalResponseCodeError.Message318;

                return Ok(response);
            }

            return Ok(response);
        }

    }
}
