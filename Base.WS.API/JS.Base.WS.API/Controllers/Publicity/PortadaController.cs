using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Publicity;
using JS.Base.WS.API.Helpers;
using JS.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
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
            using (MyDBcontext MyDB = new MyDBcontext())
            {
                var template = MyDB.Templates.Where(x => x.Operation == operation && x.IsActive == true && x.Enabled == true).FirstOrDefault();

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
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("GetNovelties")]
        public IHttpActionResult GetNovelties(string noveltyType)
        {
            var novelties = new List<NoveltiesByTypeDto>();
            var result = new List<NoveltiesByTypeDto>();

            using (MyDBcontext MyDB = new MyDBcontext())
            {
                bool isVisitorUser = MyDB.Users.Where(x => x.Id == currentUserId).Select(y => y.IsVisitorUser).FirstOrDefault();

                if (isVisitorUser)
                {
                    novelties = MyDB.Novelties.Where(x => x.IsPublic == true && x.IsEnabled == true && x.IsPublic == true && x.IsActive == true && x.NoveltyType.ShortName == noveltyType).Select(y => new NoveltiesByTypeDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Description = y.Description,
                        ImgPath = y.ImgPath,
                        ImgBase64 = string.Empty,
                        ContenTypeShort = y.ContenTypeShort,
                        ContenTypeLong = y.ContenTypeLong,
                        StartDate = y.FormattedStartDate,
                        EndDate = y.FormattedEndDate,

                    }).OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    novelties = MyDB.Novelties.Where(x => x.IsEnabled == true && x.IsActive == true && x.NoveltyType.ShortName == noveltyType).Select(y => new NoveltiesByTypeDto()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Description = y.Description,
                        ImgPath = y.ImgPath,
                        ImgBase64 = string.Empty,
                        ContenTypeShort = y.ContenTypeShort,
                        ContenTypeLong = y.ContenTypeLong,
                        StartDate = y.FormattedStartDate,
                        EndDate = y.FormattedEndDate,

                    }).OrderByDescending(x => x.Id).ToList();
                }

            }

            return Ok(novelties);
        }


        [HttpGet]
        [Route("GetImageByNoveltyId")]
        [AllowAnonymous]
        public IHttpActionResult GetImageByNoveltyId(long id, int width, int height)
        {
            var novelty = db.Novelties.Where(x => x.Id == id).FirstOrDefault();

            if (File.Exists(novelty.ImgPath))
            {
                byte[] file = JS_File.GetImgBytes(novelty.ImgPath);

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

    }
}
