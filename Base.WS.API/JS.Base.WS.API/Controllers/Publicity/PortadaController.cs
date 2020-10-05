using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Publicity;
using JS.Base.WS.API.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Publicity
{
    [Authorize]
    [RoutePrefix("api/portada")]
    public class PortadaController : ApiController
    {

        private MyDBcontext db;
        private Response response;


        public PortadaController()
        {
            db = new MyDBcontext();
            response = new Response();
        }

        private long currentUserId = CurrentUser.GetId();


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


        [HttpGet]
        [Route("GetNovelties")]
        public IHttpActionResult GetNovelties(string noveltyType)
        {
            bool isVisitorUser = db.Users.Where(x => x.Id == currentUserId).Select(y => y.IsVisitorUser).FirstOrDefault();

            var novelties = new List<NoveltiesByTypeDto>();
            var result = new List<NoveltiesByTypeDto>();

            if (isVisitorUser)
            {
                novelties = db.Novelties.Where(x => x.IsPublic == true && x.IsEnabled == true && x.IsPublic == true && x.IsActive == true && x.NoveltyType.ShortName == noveltyType).Select(y => new NoveltiesByTypeDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    ImgPath = y.ImgPath,
                }).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                novelties = db.Novelties.Where(x => x.IsEnabled == true && x.IsActive == true && x.NoveltyType.ShortName == noveltyType).Select(y => new NoveltiesByTypeDto()
                {
                    Id = y.Id,
                    Title = y.Title,
                    Description = y.Description,
                    ImgPath = y.ImgPath,
                }).OrderByDescending(x => x.Id).ToList();
            }

            foreach (var item in novelties)
            {
                item.ImgBase64 = GetStrigBase64(item.ImgPath);
                result.Add(item);
            }

            return Ok(result);
        }


        private string GetStrigBase64(string path)
        {
            string result = string.Empty;

            if (!String.IsNullOrEmpty(path))
            {
                byte[] file = File.ReadAllBytes(path);
                result = Convert.ToBase64String(file);
            }

            return result;
        }
    }
}
