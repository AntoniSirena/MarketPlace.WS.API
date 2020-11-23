using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.DTO.Response.Person;
using JS.Base.WS.API.Helpers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers
{
    [RoutePrefix("api/common")]
    [Authorize]
    public class CommonController : ApiController
    {
        MyDBcontext db = new MyDBcontext();
        long currentUserId = CurrentUser.GetId();

        [HttpGet]
        [Route("GetGenders")]
        public IHttpActionResult GetGenders()
        {
            var result = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPesonTypes")]
        public IHttpActionResult GetPesonTypes()
        {
            var result = db.PersonTypes.Where(x => x.IsActive == true).Select(x => new PersonType
            {

                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetDocumentTypes")]
        public IHttpActionResult GetDocumentTypes()
        {
            var result = db.DocumentTypes.Where(x => x.IsActive == true && x.ShowToCustomer == true).Select(y => new DocumentTypeDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetCompanyCategories")]
        public IHttpActionResult GetCompanyCategories()
        {
            var result = db.CompanyCategories.Where(x => x.IsActive == true).Select(y => new CompanyCategoryDTO
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).OrderBy(x => x.Description).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetUserTypes")]
        public IHttpActionResult GetUserTypes()
        {
            var result = db.UserTypes.Where(x => x.IsActive == true && x.ShowToCustomer == true).Select(y => new UserTypeDTO
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetConfigurationParameter")]
        public IHttpActionResult GetConfigurationParameter(string name)
        {
            var result = db.ConfigurationParameters.Where(x => x.Name == name && x.Enabled == true).Select(y => y.Value).FirstOrDefault();

            var response = JsonConvert.DeserializeObject<Object>(result);

            return Ok(response);
        }


        #region Models

        #endregion

    }
}
