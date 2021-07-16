using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.DTO.Response.Person;
using JS.Base.WS.API.Helpers;
using JS.Utilities;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
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


        [HttpGet]
        [Route("GetImageProfileByUserId")]
        [AllowAnonymous]
        public IHttpActionResult GetImageProfileByUserId(long id, int width, int height)
        {
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();

            if (File.Exists(user.Image))
            {
                byte[] file = JS_File.GetImgBytes(user.Image);

                if (width > 0 || height > 0)
                {
                    MemoryStream memstr = new MemoryStream(file);
                    Image img = Image.FromStream(memstr);

                    file = JS_File.ResizeImage(img, width, height);
                }

                JS_File.DownloadFileImg(file);
            }

            return Ok();
        }



        #region Models

        #endregion

    }
}
